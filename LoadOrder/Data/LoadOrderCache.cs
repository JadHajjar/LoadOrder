namespace LoadOrderTool.Data {
    using CO.IO;
    using System;
    using System.IO;
    using System.Xml.Serialization;
    using LoadOrderTool.Helpers;
    using System.Linq;
    using System.Collections.Generic;

    public class LoadOrderCache {
        public class Persona {
            public ulong ID;
            public string Name;
        }

        public enum DownloadStatus {
            DownloadOK,
            OutOfDate,
            NotDownloaded,
            PartiallyDownloaded,
            Gone,
        }

        public class Item {
            public string Path; // included path
            public string Name;
            public string Description;
            public string Author;
            public ulong AuthorID;
            public DateTime DateUpdatedUTC;
            public DownloadStatus Status;
            public string DownloadFailureReason;

            public void SetAuthor(ulong authorID) {
                AuthorID = authorID;
                UpdateAuthor();
            }

            public void UpdateAuthor() {
                var person = ConfigWrapper.instance.Cache.GetPersona(AuthorID);
                Author = person?.Name;
            }


            public virtual void Read(Util.SteamUtil.PublishedFileDTO dto) {
                Name = dto.Title;
                DateUpdatedUTC = dto.UpdatedUTC;
                SetAuthor(dto.AuthorID);
            }
        }

        public class Mod : Item {
            // public string AssemblyName;
        }

        public class Asset : Item {
            public string[] Tags;

            public override void Read(Util.SteamUtil.PublishedFileDTO dto) {
                base.Read(dto);
                Tags = dto.Tags;
            }
        }

        public Mod[] Mods = new Mod[0];
        public Asset[] Assets = new Asset[0];
        public List<Persona> People = new List<Persona>(100);

        private Hashtable<string, Mod> modTable_;
        private Hashtable<string, Asset> assetTable_;
        private Hashtable<ulong, Persona> peopleTable_;


        const string FILE_NAME = "LoadOrderCache.xml";
        static string DIR => DataLocation.LocalLOMData;
        static string FilePath => Path.Combine(DIR, FILE_NAME);

        internal void Serialize() {
            XmlSerializer ser = new XmlSerializer(typeof(LoadOrderCache));
            using (FileStream fs = new FileStream(FilePath, FileMode.Create, FileAccess.Write)) {
                ser.Serialize(fs, this);
            }
        }

        internal static LoadOrderCache Deserialize() {
            try {
                XmlSerializer ser = new XmlSerializer(typeof(LoadOrderCache));
                using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read)) {
                    var ret = ser.Deserialize(fs) as LoadOrderCache;
                    ret.RebuildIndeces();
                    return ret;
                }
            } catch {
                return null;
            }
        }

        public Mod GetMod(string includedPath) => modTable_?[includedPath];
        public Asset GetAsset(string includedPath) => assetTable_?[includedPath];
        public Persona GetPersona(ulong ID) => peopleTable_?[ID];

        public void RebuildIndeces() {
            modTable_ = new Hashtable<string, Mod>(Mods.ToDictionary(mod => mod.Path));
            assetTable_ = new Hashtable<string, Asset>(Assets.ToDictionary(asset => asset.Path));
            RebuildPeopleIndeces();
        }

        public void AddPerson(ulong id, string name) {
            var p = peopleTable_[id];
            if (p != null)
                p.Name = name;
            else
                People.Add(new Persona { ID = id, Name = name });
        }

        public void RebuildPeopleIndeces() {
            try {
                peopleTable_ = new Hashtable<ulong, Persona>(People.ToDictionary(persona => persona.ID));
            } catch {
                // fix duplicate key error : new value replace old.
                var ppl2 = new Dictionary<ulong, Persona>();
                foreach(var person in People) {
                    ppl2[person.ID] = person;
                }
                People = ppl2.Values.ToList();
                peopleTable_ = new Hashtable<ulong, Persona>(ppl2);
            }
        }
    }
}

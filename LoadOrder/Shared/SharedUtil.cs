namespace LoadOrderShared {
	using System.IO;
    using System.Xml;
    using System.Xml.Serialization;

    internal class SharedUtil
	{
#if TOOL2
		internal static string LocalApplicationData => LoadOrderToolTwo.Utilities.Managers.LocationManager.AppDataPath;
		internal static string LocalLOMData => Path.Combine(LocalApplicationData, "LoadOrder");
#elif TOOL
		internal static string LocalApplicationData => CO.IO.DataLocation.localApplicationData;
		internal static string LocalLOMData => CO.IO.DataLocation.LocalLOMData;
#elif IPATCH
        internal static string LocalApplicationData => LoadOrderIPatch.Patches.Entry.GamePaths.AppDataPath;
        internal static string LocalLOMData => LoadOrderIPatch.Patches.Entry.LocalLOMData;
#else
        internal static string LocalApplicationData => ColossalFramework.IO.DataLocation.localApplicationData;
        internal static string LocalLOMData => Path.Combine(LocalApplicationData, "LoadOrder");
#endif
		internal static XmlWriterSettings Indented => new XmlWriterSettings() { Indent = true };

        internal static XmlSerializerNamespaces NoNamespaces =>
            new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

        internal static void Serialize<T>(T obj, string filePath) {
            var serializer = new XmlSerializer(typeof(T));
            using (StreamWriter writer = new StreamWriter(filePath)) {
                var dirInfo = new FileInfo(filePath).Directory;
                if (!dirInfo.Exists) dirInfo.Create();
                using (var xmlWriter = new XmlTextWriter(writer)) {
                    xmlWriter.Formatting = Formatting.Indented;
                    serializer.Serialize(xmlWriter, obj, NoNamespaces);
                }
            }
        }

        public static T Deserialize<T>(string filePath) where T : class {
            XmlSerializer ser = new XmlSerializer(typeof(T));
            var dirInfo = new FileInfo(filePath).Directory;
            if (!dirInfo.Exists) dirInfo.Create();
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read)) {
                return ser.Deserialize(fs) as T;
            }
        }
    }
}

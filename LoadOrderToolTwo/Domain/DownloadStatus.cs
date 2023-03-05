namespace LoadOrderToolTwo.Domain;

public enum DownloadStatus
{
	None,
	OK,
	Unknown,
	OutOfDate,
	NotDownloaded,
	PartiallyDownloaded,
	Removed,
}
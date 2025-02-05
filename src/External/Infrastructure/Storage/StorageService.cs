using Application.Interfaces;
using Dropbox.Api;
using Dropbox.Api.Files;

namespace Infrastructure.Storage;

public class StorageService : IStorageService
{
    private readonly DropboxClient _dropboxClient;
    private readonly DropboxSettings _dropboxSettings;

    public StorageService(DropboxSettings dropboxSettings)
    {
        _dropboxSettings = dropboxSettings;
        _dropboxClient = new DropboxClient(
            _dropboxSettings.Key,
            _dropboxSettings.AppKey,
            _dropboxSettings.AppSecret
        );
    }

    public async Task<string> UploadAsync(string name, Stream stream)
    {
        var path = $"/Apps/{_dropboxSettings.Folder}/{name}";

        await _dropboxClient.Files.UploadAsync(path, WriteMode.Overwrite.Instance, body: stream);

        var sharedLink = await _dropboxClient.Sharing.CreateSharedLinkWithSettingsAsync(
            path,
            new Dropbox.Api.Sharing.SharedLinkSettings(allowDownload: true)
        );

        var url = sharedLink.Url.Replace("dl=0", "raw=1");

        return url;
    }
}

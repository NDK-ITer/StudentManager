namespace Server.Extension
{
    public class ImageCleanupService : IHostedService, IDisposable
    {
        private readonly string _tempFolderPath;
        private readonly TimeSpan _expirationPeriod = TimeSpan.FromSeconds(3); 

        public ImageCleanupService(string tempFolderPath)
        {
            _tempFolderPath = tempFolderPath;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {

            var timer = new Timer(CleanupExpiredImages, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {

        }

        private void CleanupExpiredImages(object state)
        {
            try
            {
                foreach (var filePath in Directory.GetFiles(_tempFolderPath))
                {
                    var fileInfo = new FileInfo(filePath);

                    if (DateTime.UtcNow - fileInfo.LastWriteTimeUtc > _expirationPeriod)
                    {
                        File.Delete(filePath);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

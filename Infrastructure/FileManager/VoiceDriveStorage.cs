using Domain.FileManager;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using NAudio.Wave;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NAudio.Wave;
using NAudio.Lame;

namespace Infrastructure.FileManager
{
    public class VoiceDriveStorage : IFileStorage
    {
        private readonly IWebHostEnvironment _env;

        public VoiceDriveStorage(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> StoreAsync(IFormFile file, CancellationToken cancellationToken, string path = null)
        {
            var folderPath = Path.Combine(_env.WebRootPath, path);
            var outputPath = Path.Combine(folderPath, Guid.NewGuid() + ".mp3");
            //var tempPath = Path.GetTempFileName();
            await using (var stream = new FileStream(outputPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
           

            //using var reader = new AudioFileReader(tempPath);
            //using var writer = new LameMP3FileWriter(outputPath, reader.WaveFormat, LAMEPreset.VBR_90);
           // reader.CopyTo(writer);

            //File.Delete(tempPath);

            return outputPath;

            //var ext = Path.GetExtension(file.FileName);
            //var folderPath = Path.Combine(_env.WebRootPath, path);
            //var tempName = "temp_" + Guid.NewGuid() + ext;
            //var tempPath = Path.Combine(folderPath, tempName);
            //if (!Directory.Exists(folderPath))
            //{
            //    Directory.CreateDirectory(folderPath);
            //}
            //await using(var tempStream = new FileStream(tempPath, FileMode.Create))
            //{
            //    await file.CopyToAsync(tempStream);

            //}
            //var finalName = Guid.NewGuid() + ".mp3";
            //var finalPath = Path.Combine(folderPath, finalName);
            //var ffmpegPath = @"C:\ffmpeg\bin\ffmpeg.exe"; // اگه تو PATH نیست: @"C:\ffmpeg\bin\ffmpeg.exe"
            //var arguments = $"-y -i \"{tempPath}\" -b:a 64k \"{finalPath}\"";

            //var process = new Process
            //{
            //    StartInfo = new ProcessStartInfo
            //    {
            //        FileName = ffmpegPath,
            //        Arguments = arguments,
            //        RedirectStandardError = true,
            //        UseShellExecute = false,
            //        CreateNoWindow = true
            //    }
            //};

            //process.Start();

            //// 3. گرفتن خطاهای ffmpeg برای بررسی
            //var errorOutput = await process.StandardError.ReadToEndAsync();
            //await process.WaitForExitAsync();

            //// 4. بررسی موفقیت آمیز بودن عملیات
            //if (!File.Exists(finalPath))
            //{
            //    Console.WriteLine("FFmpeg Error: " + errorOutput);
            //    throw new Exception("Failed to compress voice file.");
            //}

            //File.Delete(tempPath); // Remove original

            //return $"/{path}/{finalName}";
        }

        public async Task DeleteAsync(string relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
                return;

            var fullPath = Path.Combine(_env.WebRootPath, relativePath.TrimStart('/'));
            if (File.Exists(fullPath))
                await Task.Run(() => File.Delete(fullPath));
        }

        public void Remove(string path)
        {
            if(string.IsNullOrWhiteSpace(path))
            {
                return;
            }
            var fullPath = Path.Combine(_env.WebRootPath, path.TrimStart('/'));
            if(File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

    }
}

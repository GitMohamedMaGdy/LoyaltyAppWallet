using Loyalty.Domain.Shared.IRepositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Loyalty.DataAccess.Shared
{
    public class NotificationService
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public NotificationService(IDeviceRepository deviceRepository, IConfiguration configuration,ILogger<NotificationService>logger)
        {
            _deviceRepository = deviceRepository;
            _configuration = configuration;
            _logger = logger;
        }
        public void NotifyAppleDevices(string passTypeIdentifier, string serialNumber)
        {
            List<string> devicePushTokens = _deviceRepository.GetDevicesPushTokenList(passTypeIdentifier, serialNumber);
            foreach (string pushToken in devicePushTokens)
            {
                SendEmptyPushNotification(pushToken);
            }
        }
        private void SendEmptyPushNotification(string pushToken)
        {
            string server = _configuration.GetValue<string>("NotificationGetWay:ServerUrl");
            using (TcpClient tcpClient = new TcpClient(server, 2195))
            {
                using (SslStream sslStream = new SslStream(tcpClient.GetStream()))
                {
                    try
                    {
                        byte[] certData = File.ReadAllBytes(@"wwwroot/certificates/DsqPassCert.p12");
                        byte[] certData__ = File.ReadAllBytes(@"wwwroot/certificates/wwdc.pem");
                        X509Certificate2Collection certs = new X509Certificate2Collection();
                        X509Certificate2 cert1 = new X509Certificate2(certData, "DSSD");
                        X509Certificate2 cert2 = new X509Certificate2(certData__);
                        certs.Add(cert1);
                        certs.Add(cert2);
                        sslStream.AuthenticateAsClient(server, certs,false);
                    }
                    catch (Exception exp)
                    {
                        _logger.LogError($"Excetion at Connect to Apple Notification Server {exp}");
                    }
                    MemoryStream memoryStream = new MemoryStream();
                    BinaryWriter writer = new BinaryWriter(memoryStream);
                    writer.Write((byte)0);
                    writer.Write((byte)0);
                    writer.Write((byte)32);
                    writer.Write(StringToByteArray(pushToken.ToUpper()));
                    string payload = "{\"aps\":\"\"}";
                    writer.Write((byte)0);
                    writer.Write((byte)payload.Length);
                    byte[] b1 = Encoding.UTF8.GetBytes(payload);
                    writer.Write(b1);
                    writer.Flush();
                    byte[] array = memoryStream.ToArray();
                    sslStream.Write(array);
                    sslStream.Flush();
                    _logger.LogInformation("Send Notification to Apple Gateway done successgully");
                }
            }
          
        }
        private static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

       
    }
}

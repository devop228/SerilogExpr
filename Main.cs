using System;
using Microsoft.Extensions.Logging;

namespace SerilogExpr {
public class Main
    {
        private readonly ILogger _logger;
        public Main(ILogger<Main> logger)
        {
            _logger = logger;
        }
        public void Run()
        {
            _logger.LogInformation("Hello {name}", "Yu");
            _logger.LogInformation("Hello {Person}", new { FirstName = "Yu", LastName = "Zhu" });

            try
            {
                try
                {
                    throw new Exception("Unexpected exception occurs");
                }
                catch (Exception ex)
                {
                    var exOutter = new CustomException("A customer exception wrapping exception", ex);
                    exOutter.FirstName = "Yu";
                    exOutter.LastName = "Zhu";
                    throw exOutter;
                }
            }
            catch (CustomException ce) {
                _logger.LogInformation(ce, "Exception {TrackId} {FirstName} {LastName}"
                    , ce.TrackId, ce.FirstName, ce.LastName);
            }
            catch (Exception e) {
                _logger.LogInformation(e, "Exception with innerException");
            }

            Console.ReadKey();
        }
    }
}
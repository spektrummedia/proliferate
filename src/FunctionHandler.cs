using System;

namespace Proliferate
{
    public class FunctionHandler
    {
        protected readonly string Stage;

        public FunctionHandler()
        {
            Stage = Environment.GetEnvironmentVariable("stage");
        }
    }
}
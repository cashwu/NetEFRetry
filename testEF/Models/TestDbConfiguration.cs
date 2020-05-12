using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;

namespace testEF.Models
{
    public class TestDbConfiguration : DbConfiguration
    {
        public TestDbConfiguration()
        {
            AddInterceptor(new CommandInterceptor());

            // SetExecutionStrategy(SqlProviderServices.ProviderInvariantName,
            //                      () => new SqlAzureExecutionStrategy(3, TimeSpan.FromSeconds(3)));
            SetExecutionStrategy(SqlProviderServices.ProviderInvariantName,
                                 () => new MySqlAzureExecutionStrategy(3, TimeSpan.FromSeconds(3)));
        }
    }

    class MySqlAzureExecutionStrategy : SqlAzureExecutionStrategy
    {
        private List<int> _errorCodesToRetry = new List<int>
        {
            // 把所有想要重試的錯誤代碼加入此串列.
            18456
        };

        public MySqlAzureExecutionStrategy(int maxRetryCount, TimeSpan maxDelay)
            : base(maxRetryCount, maxDelay)
        {
        }

        protected override bool ShouldRetryOn(Exception exception)
        {
            if (exception is SqlException sqlException)
            {
                foreach (SqlError err in sqlException.Errors)
                {
                    // 如果不知道哪些錯誤狀況需要重試，先用下面這行查看錯誤代碼，然後將它加入 _errorCodesToRetry 串列.
                    Console.WriteLine("ShouldRetryOn: " + err.Message + " , " + err.Number);

                    // if (_errorCodesToRetry.Contains(err.Number))
                    // {
                    //     return true;
                    // }

                    return true;
                }
            }

            return base.ShouldRetryOn(exception);
        }
    }
}
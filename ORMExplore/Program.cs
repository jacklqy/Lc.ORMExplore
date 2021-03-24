using DAL;
using Model;
using System;
using System.Threading;

namespace ORMExplore
{
    /// <summary>
    /// 手写ORM
    /// .NetCore和Asp.NetCore绑定紧密 控制台其实没有管
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Hello World!");
                SqlHelper helper = new SqlHelper();
                var user = helper.Find<tb_log_model>(1);
                int id = helper.Insert<tb_log_model>(new tb_log_model()
                {
                    info = "test",
                    methodname = "aa",
                    mqpath = "yyyy",
                    mqpath_id = 124,
                    createtime = DateTime.Now
                });
                for (int i = 0; i < 100; i++)
                {
                    var u = helper.Find<tb_log_model>(id);
                    if (u == null)
                    {
                        Console.WriteLine($"keep moving {i}");
                    }
                    else
                    {
                        Console.WriteLine($"第{i}*500ms 同步完成");
                        break;
                    }
                    Thread.Sleep(500);//暂停500毫秒
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

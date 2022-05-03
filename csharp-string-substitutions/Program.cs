using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace csharp_interpolated_string_pre4._6
{
    class Job
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Customer[] Customers { get; set; }
    }

    class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var job = new Job
            {
                Id = 1,
                Name = "Todo",
                Description = "{Job.Name}",
                Customers = new Customer[]
                {
                    new Customer
                    {
                        Name = "{Job.Name}"
                    }
                }
            };

            Console.WriteLine(ReplaceMacro("{Job.Name} {Job.Description} { Job.Customers[0].Name } job for admin", job));
            Console.ReadLine();
        }

        public static string ReplaceMacro(string value, object @object)
        {
            return Regex.Replace(value, @"{(.+?)}",
            match => {
                var p = Expression.Parameter(@object.GetType(), @object.GetType().Name);
                var e = System.Linq.Dynamic.DynamicExpression.ParseLambda(new[] { p }, null, match.Groups[1].Value);
                return (e.Compile().DynamicInvoke(@object) ?? "").ToString();
            });
        }
    }
}



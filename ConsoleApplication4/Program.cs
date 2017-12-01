using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    class Program
    {
        static void Main(string[] args)
        {
            EmpDeptEntities entities = new EmpDeptEntities();

            // INSERT, UPDATE, DELETE -------------------------------------------------

            EMP emp = new EMP()
            {
                ENAME = "MyNewEMP",
                SAL = 10000,
                JOB = "CEO",
                DEPTNO = 40,
                COMM = 1000
            };
            entities.EMP.Add(emp);
            entities.SaveChanges();

            foreach (var e in entities.EMP)
            {
                Console.WriteLine(e.ENAME);
            }

            EMP president = entities.EMP.Single(e => e.JOB == "PRESIDENT");
            president.SAL = 9999;
            entities.SaveChanges();

            Console.WriteLine(president.ENAME + " " + president.SAL);

            EMP ford = entities.EMP.Single(e => e.ENAME == "FORD");
            entities.EMP.Remove(ford);
            entities.SaveChanges();

            // ------------------------------------------------------------------------


            var avgPerDept =
                from x in entities.EMP
                group x by x.DEPT.DNAME into g   //vs x.DEPNTO!!! 
                select new
                {
                    DeptName = g.Key,
                    Avg = g.Average(y => y.SAL + (y.COMM ?? 0))
                };

            var avgPerDept2 =
                entities.EMP.GroupBy(x => x.DEPT.DNAME).Select(
                    g => new
                    {
                        DeptName = g.Key,
                        Avg = g.Average(y => y.SAL + (y.COMM ?? 0))
                    });

            foreach (var val in avgPerDept)
            {
                Console.WriteLine(val);
            }

            Console.WriteLine("---");

            foreach (var val in avgPerDept2)
            {
                Console.WriteLine(val);
            }

            Console.WriteLine("--------------------------------");

            var deptWorkerCounts =
                from x in entities.EMP
                group x by x.DEPTNO into g
                join d in entities.DEPT on g.Key equals d.DEPTNO
                select new { DeptName = g.Key, Count = g.Count() };

            foreach (var x in deptWorkerCounts)
            {
                Console.WriteLine(x);
            }

            ////var maxWorkerDeptNo = deptWorkerCounts.OrderByDescending(g => g.Count).First().DeptNo;

            ////var maxWorkerDept =
            ////    from x in entities.DEPT
            ////    where x.DEPTNO == maxWorkerDeptNo
            ////    select x.DNAME;

            //Console.WriteLine(maxWorkerDept.First());
            Console.WriteLine("--------------------------------");

            var jobAvgSals =
                from x in entities.EMP
                group x by x.JOB into g
                select new { Name = g.Key, Avg = g.Average(y => y.SAL) };

            var jobAvgSalsSorted = jobAvgSals.OrderBy(g => g.Avg);

            foreach (var val in jobAvgSalsSorted)
            {
                Console.WriteLine(val);
            }

            Console.WriteLine("--------------------------------");

            var workersAfterPresident =
                from x in entities.EMP
                let presidentDate = entities.EMP.Where(e => e.JOB == "PRESIDENT").FirstOrDefault().HIREDATE
                let diffDays = DbFunctions.DiffDays(presidentDate, x.HIREDATE).Value
                where diffDays > 0 && diffDays < 30
                select new { Name = x.ENAME, Date = x.HIREDATE, DiffDays = diffDays };

            foreach (var val in workersAfterPresident)
            {
                Console.WriteLine(val);
            }

            Console.ReadLine();
        }
    }
}

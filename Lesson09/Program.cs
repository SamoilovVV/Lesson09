using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Lesson09
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                //ResearchCurrentThread();
                //StartNewThread();
                //StartNewParametrizedThread();
                //ThreadTypes();
                //SettingThreadPriority();
                //RaceConditiion();
                //NoRaceCondition();
                //DeadLockExample();
                //UsingMutex();
                //UsingSemaphore();

                //UsingTasks();
                //UsingTasksWithWait();
                //NestedTasks();
                //Multitasking1();
                //Multitasking2();
                //TasksWithResults();
                //TaskContinuation();
                //TaskCancelling();

                //RunVoidTaskAsync();
                //await RunTaskAsync();
                //RunTaskTAsync();
                //RunAsyncAsSync();
                //RunWhenAllAsync();
                //CancelAsyncTasks();
                //ErrorHandlingAsync();
                //ErrorHandling2Async();


                //UsingParallelFor();
                //UsingParallelForeach();

                //UsingParralelLinq();
                //UsingParallelLinqWithForAll();
                //UsingParallelLinqWithOrder();
                CancelParallelOperation();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.ReadLine();
        }

        public static void ResearchCurrentThread()
        {
            // получаем текущий поток
            Thread t = Thread.CurrentThread;

            //получаем имя потока
            Console.WriteLine($"Имя потока: {t.Name}");
            t.Name = "Метод Main";
            Console.WriteLine($"Имя потока: {t.Name}");

            Console.WriteLine($"Запущен ли поток: {t.IsAlive}");
            Console.WriteLine($"Приоритет потока: {t.Priority}");
            Console.WriteLine($"Статус потока: {t.ThreadState}");

            // получаем домен приложения
            Console.WriteLine($"Домен приложения: {Thread.GetDomain().FriendlyName}");

            // приостановка потока
            Console.WriteLine($"Приостанавливаем выполенение потока на 2 секунды");
            Thread.Sleep(2000);
            Console.WriteLine($"Выполнение потока возобновлено");
        }



        public static void StartNewThread()
        {
            Thread myThread = new Thread(Count); //Создаем новый объект потока (Thread)
            myThread.IsBackground = true;
            myThread.Start(); //запускаем поток

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Поток 1 выводит {i}");
                Thread.Sleep(100);
            }
        }

        static void Count()
        {
            for (int i = 0; i < 200; i++)
            {
                Console.WriteLine($"Поток 2 выводит {i}");
                Thread.Sleep(100);
            }
        }

        public static void StartNewParametrizedThread()
        {
            Thread myThread = new Thread(CountWithParam);

            int number = 3;
            myThread.Start(number); //запускаем поток с параметром

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Поток 1 выводит {i * number}");
                Thread.Sleep(100);
            }
        }

        static void CountWithParam(object obj)
        {
            if (obj is int n)
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine($"Поток 2 выводит {i * n}");
                    Thread.Sleep(100);
                }
            }
        }

        public static void ThreadTypes()
        {
            Thread thread1 = new Thread(BackgroundWorker);
            thread1.IsBackground = true;

            thread1.Start();

            Thread.Sleep(100);
        }

        static void BackgroundWorker()
        {
            for (int i = 0; i < 1000000; i++)
            {
                Console.WriteLine($"Фоновый поток выводит {i}");
            }
        }

        public static void SettingThreadPriority()
        {
            Thread thread1 = new Thread(Worker1);
            Thread thread2 = new Thread(Worker2);
            Thread thread3 = new Thread(Worker3);
            thread1.Priority = ThreadPriority.Highest;
            thread2.Priority = ThreadPriority.Normal;
            thread3.Priority = ThreadPriority.Lowest;

            thread1.Start();
            thread2.Start();
            thread3.Start();
        }

        static void Worker1()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Поток 1 выводит {i}");
            }
        }

        static void Worker2()
        {

            for (int i = 10; i < 20; i++)
            {
                Console.WriteLine($"Поток 2 выводит {i}");
            }
        }

        static void Worker3()
        {
            for (int i = 20; i < 30; i++)
            {
                Console.WriteLine($"Поток 3 выводит {i}");
            }
        }


        public static void RaceConditiion()
        {
            for (int i = 0; i < 5; i++)
            {
                Thread myThread = new Thread(StaticCount);
                myThread.Name = $"Поток {i}";
                myThread.Start();
            }
        }

        static int x = 0;
        static void StaticCount()
        {
            x = 1;
            for (int i = 0; i < 10; ++i)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name}: {x}");
                x++;
                Thread.Sleep(100);
            }
        }

        public static void NoRaceCondition()
        {
            for (int i = 0; i < 5; i++)
            {
                Thread myThread = new Thread(StaticCountWithLock);
                myThread.Name = $"Поток {i}";
                myThread.Start();
            }
        }

        static object locker = new object();
        static void StaticCountWithLock()
        {
            lock (locker)
            {
                x = 1;
                for (int i = 0; i < 10; ++i)
                {
                    Console.WriteLine($"{Thread.CurrentThread.Name}: {x}");
                    x++;
                    Thread.Sleep(100);
                }
            }
        }
        
        public static void DeadLockExample()
        {
            Thread thread1 = new Thread((ThreadStart)SomeMethod);
            Thread thread2 = new Thread((ThreadStart)OtherMethod);

            thread1.Start();
            thread2.Start();
        }

        static object locker1 = new object();
        static object locker2 = new object();

        static void SomeMethod()
        {
            lock (locker1)
            {
                Console.WriteLine("Some method lock locker1");
                Thread.Sleep(1000); 
                lock (locker2)
                {
                    Console.WriteLine("Some method lock locker2");
                }
            }
        }

        static void OtherMethod()
        {
            lock (locker2)
            {
                Console.WriteLine("Other method lock locker2");
                Thread.Sleep(1000); 
                lock (locker1)
                {
                    Console.WriteLine("Other method lock locker1");
                }
            }
        }

        public static void UsingMutex()
        {
            for (int i = 0; i < 5; i++)
            {
                Thread myThread = new Thread(CountWithMutex);
                myThread.Name = $"Поток {i}";
                myThread.Start();
            }
        }

        static Mutex mutexObj = new Mutex();
        static void CountWithMutex()
        {
            mutexObj.WaitOne();
            x = 1;
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name}: {x}");
                x++;
                Thread.Sleep(100);
            }
            mutexObj.ReleaseMutex();
        }

        
        static void UsingSemaphore()
        {
            for (int i = 0; i < 15; i++)
            {
                Thread myThread = new Thread(CountWithSemaphore);
                myThread.Name = $"Поток {i}";
                myThread.Start();
            }
        }

        static Semaphore semaphoreObject = new Semaphore(5, 5);
        static void CountWithSemaphore()
        {
            semaphoreObject.WaitOne();
            x = 1;
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name}: {x}");
                x++;
                Thread.Sleep(100);
            }
            semaphoreObject.Release();
        }
        public static void UsingTasks()
        {
            Task task1 = new Task(HelloTask);
            task1.Start();

            Task task2 = Task.Factory.StartNew(() => Console.WriteLine("Hello Task 2!"));

            Task task3 = Task.Run(() => Console.WriteLine("Hello Task 3!"));

            Console.WriteLine("3 задачи запущены");
        }

        static void HelloTask()
        {
            Console.WriteLine("Hello Task 1!");
        }

        public static void UsingTasksWithWait()
        {
            Task task1 = new Task(() => Console.WriteLine("Hello Task 1!"));
            task1.Start();
            
            task1.Wait();

            Task task2 = Task.Factory.StartNew(() => Console.WriteLine("Hello Task 2!"));

            task2.Wait();

            Task task3 = Task.Run(() => Console.WriteLine("Hello Task 3!"));

            task3.Wait();

            Console.WriteLine("3 задачи запущены");
        }

        public static void NestedTasks()
        {
            var outerTask = Task.Run(() =>      // внешняя задача
            {
                Console.WriteLine("Начало выполнения внешней задачи...");
                
                var innerTask = Task.Factory.StartNew(() =>  // вложенная задача
                {
                    Console.WriteLine("Начало выполнения вложенной задачи...");
                    Thread.Sleep(2000);
                    Console.WriteLine("Конец выполнения вложенной задачи.");
                }
                , TaskCreationOptions.AttachedToParent);
                
                Console.WriteLine("Конец выполнения внешней задачи.");
            });
            outerTask.Wait(); // ожидаем выполнения внешней задачи
            
            Console.WriteLine("End of NestedTask");
        }

        public static void Multitasking1()
        {
            var tasks = new Task[]
            {
                new Task(() => Console.WriteLine("First Task")),
                new Task(() => Console.WriteLine("Second Task")),
                new Task(() => Console.WriteLine("Third Task"))
            };

            foreach (var task in tasks)
            {
                task.Start();
            }

            Task.WaitAny(tasks);

            Console.WriteLine("Завершение метода Multitasking1");
        }

        public static void Multitasking2()
        {
            var tasks = new List<Task>();

            int tasksCount = 3;
            for(int i = 0; i < tasksCount; ++i)
            {
                int n = i + 1;
                tasks.Add(Task.Run(() => Console.WriteLine($"Task {n}")));
            }

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine("Завершение метода Multitasking2");
        }

        public static void TasksWithResults()
        {
            Task<Person> task = new Task<Person>(() =>
            {
                Console.WriteLine("Создаём объект Person");
                return new Person ("Вася", 22 );
            });
            task.Start();

            Person p = task.Result;  // ожидаем получение результата
            Console.WriteLine($"Имя: {p.Name}, возраст: {p.Age}");
        }

        public static void TaskContinuation()
        {
            Task<int> task1 = new Task<int>(() => Sum(2, 3));

            var task2 = task1.ContinueWith(sum => Console.WriteLine(sum.Result));

            task1.Start();

            task2.Wait();

            Console.WriteLine("Окончание процесса");
        }

        static int Sum(int a, int b)
        {
            return a + b;
        }

        static void Display(int sum)
        {
            Console.WriteLine($"Sum: {sum}");
        }

        public static void TaskCancelling()
        {
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;

            Task task1 = new Task(() => CalculateFactorial(15, token));
            task1.Start();

            Console.WriteLine("Введите Y для отмены операции или любой другой символ для ее продолжения:");
            string s = Console.ReadLine();
            if (string.Equals(s, "Y", StringComparison.CurrentCultureIgnoreCase))
            {
                cancelTokenSource.Cancel();
            }
        }

        static void CalculateFactorial(int x, CancellationToken token)
        {
            int result = 1;
            for (int i = 1; i <= x; i++)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Операция прервана токеном");
                    return;
                }

                result *= i;
                Console.WriteLine($"Факториал числа {x} равен {result}");
                Thread.Sleep(500);
            }
        }


        public static async void RunVoidTaskAsync()
        {
            await Task.Run(()=>Console.WriteLine("Awaited Task!"));
        }

        public static async Task RunTaskAsync()
        {
            for (int j = 0; j < 100; ++j)
            {
                //do something
            }

            await Task.Run(() => Console.WriteLine("Awaited Task!"));

            for (int i = 0; i < 1000; ++i)
            {
                // do something
            }
        }

        public static async void RunTaskTAsync()
        {
            int result = await FactorialAsync(4);
            Console.WriteLine($"Результат вычисления {result}");
        }

        static async Task<int> FactorialAsync(int n)
        {
            return await Task.Run(() => Factorial(n)); 
        }

        static int Factorial(int n)
        {
            if (n < 1)
                throw new ArgumentException("Значение должно быть больше нуля!");

            int result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }

        public static async void RunAsyncAsSync()
        {
            var t = Stopwatch.StartNew();

            int n1 = await FactorialAsync(5);
            int n2 = await FactorialAsync(6);
            int n3 = await FactorialAsync(7);

            t.Stop();
            Console.WriteLine($"Время выполнения {t.ElapsedMilliseconds}");

            Console.WriteLine($"{n1}\t{n2}\t{n3}");
        }

        public static async void RunWhenAllAsync()
        {
            var t = Stopwatch.StartNew();

            var t1 = FactorialAsync(5);
            var t2 = FactorialAsync(6);
            var t3 = FactorialAsync(7);
            await Task.WhenAll(new[] { t1, t2, t3 });

            t.Stop();
            Console.WriteLine($"Время выполнения {t.ElapsedMilliseconds}");

            Console.WriteLine($"{t1.Result}\t{t2.Result}\t{t3.Result}");
        }

        public static async void CancelAsyncTasks()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;

            new Task(() =>
            {
                Thread.Sleep(1000);
                cts.Cancel();
            }).Start();

            await FactorialAsync(12, token);
        }

        static async Task FactorialAsync(int n, CancellationToken token)
        {
            await Task.Run(() => CalculateFactorial(n, token));
        }

        public static async void ErrorHandlingAsync()
        {
            Task<int> task = null;
            try
            {
                task = Task.Run(()=>Factorial(-4));
                await task;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(task.Exception.InnerException.Message);
                Console.WriteLine($"IsFaulted: {task.IsFaulted}");
            }
        }

        public static async void ErrorHandling2Async()
        {
            Task tasks = null;
            try
            {
                var t1 = FactorialAsync(-3);
                var t2 = FactorialAsync(4);
                var t3 = FactorialAsync(-5);
                tasks = Task.WhenAll(new[] { t1, t2, t3 });
                await tasks;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Исключение: " + ex.Message);
                Console.WriteLine("IsFaulted: " + tasks.IsFaulted);
                foreach (var inx in tasks.Exception.InnerExceptions)
                {
                    Console.WriteLine("Внутреннее исключение: " + inx.Message);
                }
            }
        }

        public static void UsingParallelFor()
        {
            //for(int i = 0; i <5; ++i)
            //{
            //    Console.WriteLine(i);
            //}
            Parallel.For(0, 25, n=>Console.WriteLine(n));
        }

        private static void ShowNumber(int n)
        {
            Console.WriteLine(n);
        }

        public static void UsingParallelForeach()
        {
            var names = new List<string> { "Вася", "Петя", "Маша", "Гена", "Юля", "Вадим", "Сергей", "Денис", "Коля", "Женя", "Света", "Таня", "Оля" };
            Parallel.ForEach(names, n=>Console.WriteLine(n));

            ParallelLoopResult result = Parallel.ForEach(names, ShowNameWithState);
            if (!result.IsCompleted)
                Console.WriteLine($"Цикл завершён на итерации {result.LowestBreakIteration}");
            
        }

        static void ShowName(string name)
        {
            Console.WriteLine(name);
        }

        static void ShowNameWithState(string name, ParallelLoopState loopState)
        {
            Console.WriteLine(name);
            if (name == "Денис")
                loopState.Break();

        }

        public static void UsingParralelLinq()
        {
            var names = new List<string> { "Вася", "Петя", "Маша", "Гена", "Юля", "Вадим", "Сергей", "Денис", "Коля", "Женя", "Света", "Таня", "Оля" };

            IEnumerable<string> selectedNames = names.Where(n => n.Contains("а"));

            Console.WriteLine("Результат последовательного запроса:");
            foreach (string s in selectedNames)
            {
                Console.WriteLine(s);
            }

            selectedNames = names.AsParallel().Where(n => n.Contains("а"));
            Console.WriteLine("Результат параллельного запроса:");
            foreach (string s in selectedNames)
            {
                Console.WriteLine(s);
            }
        }

        public static void UsingParallelLinqWithForAll()
        {
            var names = new List<string> { "Вася", "Петя", "Маша", "Гена", "Юля", "Вадим", "Сергей", "Денис", "Коля", "Женя", "Света", "Таня", "Оля" };

            names.AsParallel().Where(n => n.Contains("а")).ForAll(n => Console.WriteLine(n));
        }

        public static void UsingParallelLinqWithOrder()
        {
            var names = new List<string> { "Вася", "Петя", "Маша", "Гена", "Юля", "Вадим", "Сергей", "Денис", "Коля", "Женя", "Света", "Таня", "Оля" };

            IEnumerable<string> selectedNames = names.Where(n => n.Contains("а"));

            Console.WriteLine("Результат последовательного запроса:");
            foreach (string s in selectedNames)
            {
                Console.WriteLine(s);
            }


            selectedNames = names.AsParallel().AsOrdered().Where(n => n.Contains("а"));

            Console.WriteLine("Результат упорядоченного запроса:");
            foreach (string s in selectedNames)
            {
                Console.WriteLine(s);
            }

            selectedNames = names.AsParallel().Where(n => n.Contains("а")).OrderBy(n=>n);

            Console.WriteLine("Результат отсортированного запроса:");
            foreach (string s in selectedNames)
            {
                Console.WriteLine(s);
            }
        }

        public static void CancelParallelOperation()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            new Task(() =>
            {
                Thread.Sleep(200);
                cts.Cancel();
            }).Start();

            try
            {
                var names = new List<string> { "Вася", "Петя", "Маша", "Гена", "Юля", "Вадим", "Сергей", "Денис", "Коля", "Женя", "Света", "Таня", "Оля" };
                var selectedNames = names.AsParallel().WithCancellation(cts.Token).Where(n => { return n.Contains("а"); });
                foreach (string s in selectedNames)
                    Console.WriteLine(s);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Операция была прервана");
            }
            catch (AggregateException ex)
            {
                if (ex.InnerExceptions != null)
                {
                    foreach (Exception e in ex.InnerExceptions)
                        Console.WriteLine(e.Message);
                }
            }
            //finally
            //{
            //    cts.Dispose();
            //}
        }
    }
}

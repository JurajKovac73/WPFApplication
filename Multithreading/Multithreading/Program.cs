// Synchronization primitives
//barrier signalandwait caka kym cislo taskov nebude splnene, da na nulu a znova.
//countdown zvlast signal a zvlast wait podobne ako barrier len ma cislo taskov ktore sa musia vykonat kym pusti task dalsi
//ManualResetEventSlim jeden event - .set a .wait kolko krat chces
//AutoResetEvent(false) je tiez set wait ale po wait sa da set späť na false WaiOne()
//SemaphoreSlim(2,10); range of how much threads can simultianosly access

int sum = 0;
Parallel.For(1, 1001, () => 0,
    (x, state, currentValueOfThreadLocalStorage) =>
    {
        currentValueOfThreadLocalStorage += x;
   //     Console.WriteLine($"Task {Task.CurrentId} has sum {currentValueOfThreadLocalStorage}");
        return currentValueOfThreadLocalStorage;
    }, partialSum =>
    {

     //   Console.WriteLine($"Partial value of task {Task.CurrentId} has sum {partialSum}");

        Interlocked.Add(ref sum, partialSum);
    });
Console.WriteLine($"sum of 1...1000 = {sum}");


/*
var planned = new CancellationTokenSource();
var preventative = new CancellationTokenSource();
var emergency = new CancellationTokenSource();

var paranoid = CancellationTokenSource.CreateLinkedTokenSource(
    planned.Token, preventative.Token, emergency.Token);

Task.Factory.StartNew(() =>
{
    int i = 0;
    while (true) {
        paranoid.Token.ThrowIfCancellationRequested();
        Console.WriteLine($"{i++}\t");
        Thread.Sleep(1000);

        //block thread while spleep
       // SpinWait.SpinUntil();
    }
}, paranoid.Token);

Console.ReadKey();
emergency.Cancel();
*/

var tasks = new List<Task>();
var bankAccount = new BankAccount2();
var bankAccount2 = new BankAccount2();

/*
for(int i = 0; i < 10; i++)
{
    tasks.Add(Task.Factory.StartNew(() =>
    {
        for (int j = 0; j < 1000; j++)
        {
            bankAccount.Deposit(100);
        }
    }));

    tasks.Add(Task.Factory.StartNew(() =>
    {
        for (int j = 0; j < 1000; j++)
        {
            bankAccount.WithDraw(100);
        }
    }));
}

Task.WaitAll(tasks.ToArray());

*/


/*
SpinLock spinLock = new SpinLock();

for (int i = 0; i < 10; i++)
{
    tasks.Add(Task.Factory.StartNew(() =>
    {
        for (int j = 0; j < 1000; j++)
        {
            bool lockTaken = false;
            try {

                spinLock.Enter(ref lockTaken);
                bankAccount.Deposit(100);
            }
            finally
            {
                if(lockTaken) spinLock.Exit();
            }
            
        }
    }));

    tasks.Add(Task.Factory.StartNew(() =>
    {
        for (int j = 0; j < 1000; j++)
        {
            bool lockTaken = false;
            try {

                spinLock.Enter(ref lockTaken);
                bankAccount.Deposit(100);
            }
            finally
            {
                if(lockTaken) WithDraw.Exit();
            }
            
        }

    }));
}
*/
Mutex mutex = new Mutex();
Mutex mutex2 = new Mutex();
for (int i = 0; i < 10; i++)
{
    tasks.Add(Task.Factory.StartNew(() =>
    {
        for (int j = 0; j < 1000; j++)
        {
            bool haveLock = mutex.WaitOne();
            try
            {
                bankAccount.Deposit(100);
            }
            finally
            {
                if (haveLock) mutex.ReleaseMutex();
            }

        }
    }));

    tasks.Add(Task.Factory.StartNew(() =>
    {
        for (int j = 0; j < 1000; j++)
        {
            bool haveLock = mutex2.WaitOne();
            try
            {
                bankAccount2.Deposit(100);
            }
            finally
            {
                if (haveLock) mutex2.ReleaseMutex();
            }

        }
    }));


  /*  tasks.Add(Task.Factory.StartNew(() =>
    {
        for (int j = 0; j < 1000; j++)
        {
            bool haveLock = mutex.WaitOne();
            try
            {
                bankAccount.WithDraw(100);
            }
            finally
            {
                if (haveLock) mutex.ReleaseMutex();
            }

        }

    }));*/
    tasks.Add(Task.Factory.StartNew(() =>
    {
        for (int j = 0; j < 1000; i++)
        {
            bool haveLock = WaitHandle.WaitAll(new[] { mutex, mutex2 });
            try
            {
                bankAccount.Transfer(bankAccount2, 100);
            }
            finally
            {
                if (haveLock)
                {
                    mutex.ReleaseMutex();
                    mutex2.ReleaseMutex();
                }
            }
        }
    }));
}

Task.WaitAll(tasks.ToArray());
Console.WriteLine($"Final balance is {bankAccount.Balance}");
Console.WriteLine($"Final balance2 is {bankAccount2.Balance}");

public class BankAccount
{
    public object padlock = new object();
    public int Balance { get; private set; }
    public void Deposit(int amount)
    {
        lock (padlock)
        {
            Balance += amount;
        }
    }
    public void WithDraw(int amount)
    {
        lock (padlock)
        {
            Balance -= amount;
        }
    }

}

public class BankAccount2
{
    private int balance;
    public int Balance { get { return balance; } private set { balance = value; } }
    public void Deposit(int amount)
    {
       Interlocked.Add(ref balance, amount);

        //will not execute task below command until commands before are not done
       // Interlocked.MemoryBarrier();
      //  Thread.MemoryBarrier();
    }
    public void WithDraw(int amount)
    {

        Interlocked.Add(ref balance, -amount);

    }

    public void Transfer(BankAccount2 where, int amount)
    {
        Balance -= amount;
        where.Balance += amount;
    }

}
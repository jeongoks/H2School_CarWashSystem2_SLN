using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace H1School_CarWashSystem1
{
    public class WashingSystem
    {
        private bool washStartFlag = false;
        CancellationTokenSource token1 = new CancellationTokenSource();
        private WashingHall[] WashingHalls { get; set; }
        public List<Vehicle> VehicleInHall { get; set; }
        public List<WashingType> WashType { get; set; }
        public List<Processes> Processes { get; set; }

        public WashingSystem(int numberOfWashinghalls)
        {
            WashType = new List<WashingType>();
            Processes = new List<Processes>();
            VehicleInHall = new List<Vehicle>();
            WashingHalls = new WashingHall[numberOfWashinghalls];

            // This is where we create the amount of Washing Halls wanted.
            for (int i = 0; i < WashingHalls.Length; i++)
            {
                WashingHalls[i] = new WashingHall(i + 1);
            }

        }

        /// <summary>
        /// This is where we check for any available Washing Halls.
        /// </summary>
        /// <returns></returns>
        public List<WashingHall> CheckAvailableWashHall()
        {
            List<WashingHall> freeHalls = new List<WashingHall>();

            foreach (WashingHall item in WashingHalls)
            {
                if (item.VehicleInHall == false)
                {
                    freeHalls.Add(item);
                }
            }
            return freeHalls;
        }

        /// <summary>
        /// Finds the specific Washing halls.
        /// </summary>
        /// <param name="hallName"></param>
        /// <returns></returns>
        public WashingHall FindWashingHall(int hallName)
        {
            foreach (WashingHall item in WashingHalls)
            {
                if (item.Id == (int)hallName)
                {
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// Finding a specific Wasing Type.
        /// </summary>
        /// <param name="inputProgramId"></param>
        /// <returns></returns>
        public WashingType FindWashingType(int inputProgramId)
        {
            foreach (WashingType item in WashType)
            {
                if (item.Id == inputProgramId)
                {
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// Shows what to choose what the specific Washing Types.
        /// </summary>
        public void ShowProgramTypes()
        {
            foreach (WashingType item in WashType)
            {
                Console.WriteLine($"Choose {item.Id} for {item.Types}.");
            }
        }

        public void CreateTypesAndProcesses()
        {
            AddProcessesToGoldProgram();
            AddProcessToSilverProgram();
            AddProcessToStandardProgram();
            CreateGoldProgramType();
            CreateSilverProgramType();
            CreateStandardProgram();
        }

        #region Creating the different Washing Types.
        public void CreateGoldProgramType()
        {
            WashingType goldProgram = new WashingType();
            goldProgram.Prices = WashingType.Price.GoldPrice;
            goldProgram.Types = WashingType.WashType.Gold;
            goldProgram.Id = 1;
            goldProgram.Processes.Add(Processes[0]);
            WashType.Add(goldProgram);
        }

        public void CreateSilverProgramType()
        {
            WashingType silverProgram = new WashingType();
            silverProgram.Prices = WashingType.Price.SilverPrice;
            silverProgram.Types = WashingType.WashType.Silver;
            silverProgram.Id = 2;
            silverProgram.Processes.Add(Processes[1]);
            WashType.Add(silverProgram);
        }

        public void CreateStandardProgram()
        {
            WashingType standardProgram = new WashingType();
            standardProgram.Prices = WashingType.Price.StandardPrice;
            standardProgram.Types = WashingType.WashType.Standard;
            standardProgram.Id = 3;
            standardProgram.Processes.Add(Processes[2]);
            WashType.Add(standardProgram);
        }
        #endregion

        #region Adding the Processes to our Program Types.
        public void AddProcessesToGoldProgram()
        {
            Processes goldProcess = new Processes();
            goldProcess.Rinsing = true;
            goldProcess.Waxing = true;
            goldProcess.UndercarriageRinse = true;
            goldProcess.Washing = true;
            goldProcess.Drying = true;
            Processes.Add(goldProcess);
        }

        public void AddProcessToSilverProgram()
        {
            Processes silverProcess = new Processes();
            silverProcess.Rinsing = true;
            silverProcess.Waxing = true;
            silverProcess.Washing = true;
            silverProcess.Drying = true;
            Processes.Add(silverProcess);
        }

        public void AddProcessToStandardProgram()
        {
            Processes standardProcess = new Processes();
            standardProcess.Rinsing = true;
            standardProcess.Washing = true;
            standardProcess.Drying = true;
            Processes.Add(standardProcess);
        }
        #endregion

        /// <summary>
        ///  This is where we're starting our washing progress.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="inputProgramId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async void StartWash(int name, int inputProgramId, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                WashingHall washingHall = FindWashingHall(name);
                washingHall.Cycle = true;
                washingHall.VehicleInHall = true;
                Console.SetCursorPosition(0, 10);
                WashingType chosenType = FindWashingType(inputProgramId);
                try
                {
                    WashInProgress(cancellationToken, new Progress<ImportProgress>(DisplayProgress), washingHall, inputProgramId);
                }
                catch (OperationCanceledException)
                {

                    Console.WriteLine("The Wash was canceled.");
                    washingHall.VehicleInHall = false;
                }
            }, cancellationToken);
        }

        /// <summary>
        /// Here we'll have the Washing Halls run with their Washes without depending on the others in progress.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="progressObserver"></param>
        /// <param name="hallId"></param>
        /// <param name="programType"></param>
        /// <returns></returns>
        public void WashInProgress(CancellationToken cancellationToken, IProgress<ImportProgress> progressObserver, WashingHall hallId, int programType)
        {
            washStartFlag = true;
            if (hallId.Id == 1)
            {
                for (int i = 1; i < 101; i++)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    #region If statements depending on the Program Type.
                    if (programType == 1)
                    {
                        progressObserver.Report(new ImportProgress { OverallProgress = i, WashingHall = hallId.Id });
                        Thread.Sleep(2000);
                    }
                    if (programType == 2)
                    {
                        progressObserver.Report(new ImportProgress { OverallProgress = i, WashingHall = hallId.Id });
                        Thread.Sleep(1000);
                    }
                    if (programType == 3)
                    {
                        progressObserver.Report(new ImportProgress { OverallProgress = i, WashingHall = hallId.Id });
                        Thread.Sleep(500);
                    }
                    #endregion
                }
            }
            if (hallId.Id == 2)
            {
                for (int i = 1; i < 101; i++)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    #region If statements depending on the Program Type.
                    if (programType == 1)
                    {
                        progressObserver.Report(new ImportProgress { OverallProgress = i, WashingHall = hallId.Id });
                        Thread.Sleep(2000);
                    }
                    if (programType == 2)
                    {
                        progressObserver.Report(new ImportProgress { OverallProgress = i, WashingHall = hallId.Id });
                        Thread.Sleep(1000);
                    }
                    if (programType == 3)
                    {
                        progressObserver.Report(new ImportProgress { OverallProgress = i, WashingHall = hallId.Id });
                        Thread.Sleep(500);
                    }
                    #endregion
                }
            }
            if (hallId.Id == 3)
            {
                for (int i = 1; i < 101; i++)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    #region If statements depending on the Program Type.
                    if (programType == 1)
                    {
                        progressObserver.Report(new ImportProgress { OverallProgress = i, WashingHall = hallId.Id });
                        Thread.Sleep(2000);
                    }
                    if (programType == 2)
                    {
                        progressObserver.Report(new ImportProgress { OverallProgress = i, WashingHall = hallId.Id });
                        Thread.Sleep(1000);
                    }
                    if (programType == 3)
                    {
                        progressObserver.Report(new ImportProgress { OverallProgress = i, WashingHall = hallId.Id });
                        Thread.Sleep(500);
                    }
                    #endregion
                }
            }

            hallId.VehicleInHall = false;
        }

        private static readonly object _lock = new object();

        /// <summary>
        /// Here is where we'll Display the progress in %.
        /// </summary>
        /// <param name="progress"></param>
        public void DisplayProgress(ImportProgress progress)
        {
            lock (_lock)
            {
                Console.SetCursorPosition(70, progress.WashingHall);
                Console.WriteLine($"Washing Hall: {progress.WashingHall} has reached {progress.OverallProgress}%.");
            }
        }
    }
}

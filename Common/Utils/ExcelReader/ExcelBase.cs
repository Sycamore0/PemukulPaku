using Newtonsoft.Json;

namespace Common.Utils.ExcelReader
{
#pragma warning disable CS8618, CS8602 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public abstract class BaseExcelReader<Self, Scheme>
    {
        public Scheme[] All { get; set; }
        private readonly Logger c = new("Factory", ConsoleColor.Yellow);
        public abstract string FileName { get; }
        private static Self Instance;

        public static Self GetInstance()
        {
            Instance ??= Activator.CreateInstance<Self>();

            if ((Instance as BaseExcelReader<Self, Scheme>).All == null)
            {
                (Instance as BaseExcelReader<Self, Scheme>).Load();
                if((int)Global.config.VerboseLevel > 1)
                    (Instance as BaseExcelReader<Self, Scheme>).c.Log($"{typeof(Self).Name} Excel Loaded From {(Instance as BaseExcelReader<Self, Scheme>).FileName}");
            }

            return Instance;
        }

        public void Load()
        {
            All = JsonConvert.DeserializeObject<Scheme[]>(File.ReadAllText($"Resources\\ExcelOutputAsset\\{FileName}")) ?? Array.Empty<Scheme>();
        }
#pragma warning restore CS8618, CS8602 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}

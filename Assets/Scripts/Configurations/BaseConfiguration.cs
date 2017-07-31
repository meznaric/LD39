

namespace BaseConfigurations {
    public enum Difficulty {Easy, Medium, Hard};
    class BaseConfiguration {
        public Difficulty difficulty;

        public float startPower;
        public float hardnessTermFactor;
        public int negativePullFactor;

        // Figure configuration
        public int figurePower;
        public float enemyFigurePower; // Example for 0.5:figurePower * 0.5 = enemyFigurePower
        public int speakerDurationSteps;
        public int microphoneDurationSteps;

        public int speakerPowerChange;
        public int microphonePowerChange;

        // Costs
        // More upgrades has a fixed cost
        public int moreUpgradesCost;
        // Clock is paid with total percent of all sizes together
        public float clockPercCost;

        // Spin story winnings - Random number between these two variables
        public int spinStoryMaxWin;
        public int spinStoryMinWin;

        // PowerUp Config
        public int maxPowerUplevel;

        // Timings
        public int termDurationInSteps;
        public int randomEventIntervalInSec;
        public int powerUpEveryStep;
        public float cpuStepEverySec;
        public static BaseConfiguration[] baseConfigurations;
        public static void GenerateBaseConfigurations() {
            baseConfigurations = new BaseConfiguration[] {
                new BaseConfiguration {
                    difficulty = Difficulty.Easy,
                    hardnessTermFactor = 2f,
                    negativePullFactor = 1,
                    figurePower = 15,
                    enemyFigurePower = 0.2f,
                    moreUpgradesCost = 300,
                    clockPercCost = 0.03f,
                    spinStoryMaxWin = 2300,
                    spinStoryMinWin = 1300,
                    maxPowerUplevel = 8,
                    termDurationInSteps = 64,
                    randomEventIntervalInSec = 16,
                    powerUpEveryStep = 16,
                    cpuStepEverySec = 9f,
                    speakerDurationSteps = 24,
                    microphoneDurationSteps = 12,
                    microphonePowerChange = 18,
                    speakerPowerChange = 10,
                    startPower = 0.75f
                },
                new BaseConfiguration {
                    difficulty = Difficulty.Medium,
                    hardnessTermFactor = 3,
                    negativePullFactor = 1,
                    figurePower = 15,
                    enemyFigurePower = 0.3f,
                    moreUpgradesCost = 500,
                    clockPercCost = 0.04f,
                    spinStoryMaxWin = 2000,
                    spinStoryMinWin = 1000,
                    maxPowerUplevel = 6,
                    termDurationInSteps = 48,
                    randomEventIntervalInSec = 16,
                    powerUpEveryStep = 16,
                    cpuStepEverySec = 6f,
                    speakerDurationSteps = 20,
                    microphoneDurationSteps = 10,
                    microphonePowerChange = 18,
                    speakerPowerChange = 10,
                    startPower = 0.60f
                },
                new BaseConfiguration {
                    difficulty = Difficulty.Hard,
                    hardnessTermFactor = 4,
                    negativePullFactor = 2,
                    figurePower = 10,
                    enemyFigurePower = 0.3f,
                    moreUpgradesCost = 300,
                    clockPercCost = 0.04f,
                    spinStoryMaxWin = 1200,
                    spinStoryMinWin = 800,
                    maxPowerUplevel = 6,
                    termDurationInSteps = 48,
                    randomEventIntervalInSec = 16,
                    powerUpEveryStep = 10,
                    cpuStepEverySec = 3f,
                    speakerDurationSteps = 32,
                    microphoneDurationSteps = 3,
                    microphonePowerChange = 40,
                    speakerPowerChange = 10,
                    startPower = 0.45f
                }
            };
        }

        public object Clone() {
            return this.MemberwiseClone();
        }
    }
}


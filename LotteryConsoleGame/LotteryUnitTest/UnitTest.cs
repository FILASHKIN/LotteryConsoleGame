using System;
using LotteryGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LotteryUnitTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void CheckDataLotteryResultWithTarget()
        {
            int range = new Random().Next(int.MinValue, int.MaxValue);
            int targetValue = new Random().Next(int.MinValue, int.MaxValue);

            LotteryResult result = Lottery.GetLotteryResult(targetValue, range);

            Assert.IsTrue(result.targetValue >= 0);

            if (range > 0)
                Assert.IsTrue(result.randomRange > 0);
            else Assert.AreEqual(0, result.randomRange);
        }

        [TestMethod]
        public void CheckDataLotteryResultWithoutTarget()
        {
            int range = new Random().Next(int.MinValue, int.MaxValue);

            LotteryResult result = Lottery.GetLotteryResult(range);

            Assert.IsTrue(result.targetValue == 0);

            if (range > 0)
                Assert.IsTrue(result.randomRange > 0);
            else Assert.AreEqual(0, result.randomRange);
        }

        [TestMethod]
        public void CheckBooleanToByte()
        {
            Assert.AreEqual(1, Lottery.BooleanToByte(true));
            Assert.AreEqual(0, Lottery.BooleanToByte(false));
        }

        [TestMethod]
        public void CheckByteToBoolean()
        {
            sbyte a = (sbyte)new Random().Next(sbyte.MinValue, sbyte.MaxValue);

            if (a > 0)
                Assert.IsTrue(Lottery.ByteToBoolean(a));
            else Assert.IsFalse(Lottery.ByteToBoolean(a));
        }

        [TestMethod]
        public void CheckBooleanLotteryResult()
        {
            int range = new Random().Next(-15, 15);
            int targetValue = new Random().Next(-20, 20);

            LotteryResult result = Lottery.GetLotteryResult(targetValue, range);

            if (result.randomResult == result.targetValue)
                Assert.IsTrue(Lottery.LotteryResultBoolean(result));
            else Assert.IsFalse(Lottery.LotteryResultBoolean(result));
        }

        [TestMethod]
        public void CheckBinaryLotteryResult()
        {
            int range = new Random().Next(-15, 15);
            int targetValue = new Random().Next(-20, 20);

            LotteryResult result = Lottery.GetLotteryResult(targetValue, range);

            if (result.randomResult == result.targetValue)
                Assert.AreEqual(1, Lottery.LotteryResultBinary(result));
            else Assert.AreEqual(0, Lottery.LotteryResultBinary(result));
        }
    }
}

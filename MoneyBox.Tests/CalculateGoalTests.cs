using System;
using Xunit;

namespace MoneyBox.Tests
{
    public class CalculateGoalTests
    {
        [Fact]
        public void CalculateGoal_MonthlyDepositZero_ReturnsError()
        {
            var (months, date, error) = Program.CalculateGoal(100m, 0m, 200m);
            Assert.Equal(0, months);
            Assert.Equal(DateTime.MinValue, date);
            Assert.False(string.IsNullOrEmpty(error));
        }

        [Fact]
        public void CalculateGoal_AlreadyAtTarget_ReturnsZeroAndToday()
        {
            var (months, date, error) = Program.CalculateGoal(1000m, 100m, 500m);
            Assert.Equal(0, months);
            Assert.Equal(DateTime.Today, date);
            Assert.False(string.IsNullOrEmpty(error));
        }

        [Fact]
        public void CalculateGoal_TypicalCase_ReturnsCorrectMonthsAndDate()
        {
            // initial 100, deposit 100, target 450 -> remaining 350 -> 4 months (ceil(3.5))
            var (months, date, error) = Program.CalculateGoal(100m, 100m, 450m);
            Assert.Equal(4, months);
            Assert.Equal(DateTime.Today.AddMonths(4), date);
            Assert.True(string.IsNullOrEmpty(error));
        }

        [Fact]
        public void CalculateGoal_LargeNumbers_ReturnsLargeMonths()
        {
            var (months, date, error) = Program.CalculateGoal(0m, 1m, 10000m);
            Assert.Equal(10000, months);
            Assert.Equal(DateTime.Today.AddMonths(10000), date);
            Assert.True(string.IsNullOrEmpty(error));
        }
    }
}

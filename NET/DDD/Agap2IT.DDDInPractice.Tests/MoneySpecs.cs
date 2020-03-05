using System;
using Agap2IT.DDDInPractice.Logic;
using FluentAssertions;
using Xunit;

namespace Agap2IT.DDDInPractice.Tests {
    public class MoneySpecs {
        [Fact]
        public void Sum_of_two_moneys_produces_correct_result () {
            // Arrange
            Money money1 = new Money (1, 2, 3, 4, 5, 6);
            Money money2 = new Money (1, 2, 3, 4, 5, 6);

            // Act
            Money sum = money1 + money2;

            // Assert
            sum.OneCentCount.Should ().Be (2);
            sum.TenCentCount.Should ().Be (4);
            sum.QuarterCount.Should ().Be (6);
            sum.OneEuroCount.Should ().Be (8);
            sum.FiveEuroCount.Should ().Be (10);
            sum.TwentyEuroCount.Should ().Be (12);

        }

        [Fact]
        public void Two_money_instances_equal_if_contain_the_same_money_amount () {
            Money money1 = new Money (1, 2, 3, 4, 5, 6);
            Money money2 = new Money (1, 2, 3, 4, 5, 6);

            money1.Should ().Be (money2);
        }

        [Fact]
        public void Two_money_instances_do_not_equal_if_contains_different_money_amount () {
            Money euro = new Money (0, 0, 0, 1, 0, 0);
            Money hundredCents = new Money (100, 0, 0, 0, 0, 0);
            euro.Should ().NotBe (hundredCents);
            /*
            euro.GetHashCode().Should().NotBe(hundredCents.GetHashCode());
            */
        }

        [Theory]
        [InlineData (-1, 0, 0, 0, 0, 0)]
        [InlineData (0, -2, 0, 0, 0, 0)]
        [InlineData (0, 0, -3, 0, 0, 0)]
        [InlineData (0, 0, 0, -4, 0, 0)]
        [InlineData (0, 0, 0, 0, -5, 0)]
        [InlineData (0, 0, 0, 0, 0, -6)]
        public void Cannot_create_money_with_negative_value
            (
                int oneCentCount,
                int tenCentCount,
                int quarterCount,
                int oneEuroCount,
                int fiveEuroCount,
                int twentyEuroCount
            ) {

                Action action = () => new Money (
                    oneCentCount,
                    tenCentCount,
                    quarterCount,
                    oneEuroCount,
                    fiveEuroCount,
                    twentyEuroCount
                );

                action.Should ().Throw<InvalidOperationException> ();

            }

        [Theory]
        [InlineData (0, 0, 0, 0, 0, 0, 0)]
        [InlineData (1, 0, 0, 0, 0, 0, 0.01)]
        [InlineData (1, 2, 0, 0, 0, 0, 0.21)]
        [InlineData (1, 2, 3, 0, 0, 0, 0.96)]
        [InlineData (1, 2, 3, 4, 0, 0, 4.96)]
        [InlineData (1, 2, 3, 4, 5, 0, 29.96)]
        [InlineData (1, 2, 3, 4, 5, 6, 149.96)]
        public void Amount_is_calculated_correctly
            (
                int oneCentCount,
                int tenCentCount,
                int quarterCount,
                int oneEuroCount,
                int fiveEuroCount,
                int twentyEuroCount,
                decimal expectedAmount
            ) {

                Money money = new Money (
                    oneCentCount,
                    tenCentCount,
                    quarterCount,
                    oneEuroCount,
                    fiveEuroCount,
                    twentyEuroCount
                );

                money.Amount.Should ().Be (expectedAmount);
            }

        [Fact]
        public void Cannot_subtract_more_than_exists () {
            Money money1 = new Money (0, 1, 0, 0, 0, 0);
            Money money2 = new Money (1, 0, 0, 0, 0, 0);

            Action action = () => {
                Money money = money1 - money2;
            };

            action.Should ().Throw<InvalidOperationException> ();
            /*
            euro.GetHashCode().Should().NotBe(hundredCents.GetHashCode());
            */
        }
    }
}
using WebApi.Data;

namespace WebApi.Test.Methods;

public class CalculateLoyaltyPointsTest
{
    [Fact]
    public void WhenDiscountIsNotZeroAndEarnLessThanProductMaxPoints_CalculateLoyaltyPoints_ShouldBeReturnCorrectResult()
    {
        decimal totalPointsToBeEarned = 0;
        decimal orderTotalAmount = 150;
        decimal orderPayment = 120;
        decimal productPrice = 150;
        int quantity = 1;
        decimal productPercentageOfPoints = 10;
        decimal productMaxPointsEarned = 16;

        decimal totalDiscount = orderTotalAmount - orderPayment;
        decimal discountPercentage = 100 * totalDiscount / orderTotalAmount;

        if (discountPercentage != 0)
        {
            decimal price = quantity * productPrice;
            decimal priceAfterDiscount = price - (price * discountPercentage / 100);
            decimal pointsToBeEarned = priceAfterDiscount * productPercentageOfPoints / 100;
            decimal totalMaxPoints = productMaxPointsEarned * quantity;

            if (pointsToBeEarned >= totalMaxPoints)
                totalPointsToBeEarned += totalMaxPoints;
            else
                totalPointsToBeEarned += pointsToBeEarned;
        }
        else
        {
            decimal price = quantity * productPrice;
            decimal pointsToBeEarned = price * productPercentageOfPoints / 100;
            decimal totalMaxPoints = productMaxPointsEarned * quantity;

            if (pointsToBeEarned >= totalMaxPoints)
                totalPointsToBeEarned += totalMaxPoints;
            else
                totalPointsToBeEarned += pointsToBeEarned;
        }

        Assert.Equal(12, totalPointsToBeEarned);
    }

    [Fact]
    public void WhenDiscountIsNotZeroAndEarnGreaterThanProductMaxPoints_CalculateLoyaltyPoints_ShouldBeReturnCorrectResult()
    {
        decimal totalPointsToBeEarned = 0;
        decimal orderTotalAmount = 150;
        decimal orderPayment = 120;
        decimal productPrice = 150;
        int quantity = 1;
        decimal productPercentageOfPoints = 20;
        decimal productMaxPointsEarned = 16;

        decimal totalDiscount = orderTotalAmount - orderPayment;
        decimal discountPercentage = 100 * totalDiscount / orderTotalAmount;

        if(discountPercentage != 0)
        {
            decimal price = quantity * productPrice;
            decimal priceAfterDiscount = price - (price * discountPercentage / 100);
            decimal pointsToBeEarned = priceAfterDiscount * productPercentageOfPoints / 100;
            decimal totalMaxPoints = productMaxPointsEarned * quantity;

            if (pointsToBeEarned >= totalMaxPoints)
                totalPointsToBeEarned += totalMaxPoints;
            else
                totalPointsToBeEarned += pointsToBeEarned;
        }
        else
        {
            decimal price = quantity * productPrice;
            decimal pointsToBeEarned = price * productPercentageOfPoints / 100;
            decimal totalMaxPoints = productMaxPointsEarned * quantity;

            if (pointsToBeEarned >= totalMaxPoints)
                totalPointsToBeEarned += totalMaxPoints;
            else
                totalPointsToBeEarned += pointsToBeEarned;
        }

        Assert.Equal(16, totalPointsToBeEarned);
    }

    [Fact]
    public void WhenDiscountIsZeroAndEarnLessThanProductMaxPoints_CalculateLoyaltyPoints_ShouldBeReturnCorrectResult()
    {
        decimal totalPointsToBeEarned = 0;
        decimal orderTotalAmount = 150;
        decimal orderPayment = 150;
        decimal productPrice = 150;
        int quantity = 1;
        decimal productPercentageOfPoints = 10;
        decimal productMaxPointsEarned = 16;

        decimal totalDiscount = orderTotalAmount - orderPayment;
        decimal discountPercentage = 100 * totalDiscount / orderTotalAmount;

        if (discountPercentage != 0)
        {
            decimal price = quantity * productPrice;
            decimal priceAfterDiscount = price - (price * discountPercentage / 100);
            decimal pointsToBeEarned = priceAfterDiscount * productPercentageOfPoints / 100;
            decimal totalMaxPoints = productMaxPointsEarned * quantity;

            if (pointsToBeEarned >= totalMaxPoints)
                totalPointsToBeEarned += totalMaxPoints;
            else
                totalPointsToBeEarned += pointsToBeEarned;
        }
        else
        {
            decimal price = quantity * productPrice;
            decimal pointsToBeEarned = price * productPercentageOfPoints / 100;
            decimal totalMaxPoints = productMaxPointsEarned * quantity;

            if (pointsToBeEarned >= totalMaxPoints)
                totalPointsToBeEarned += totalMaxPoints;
            else
                totalPointsToBeEarned += pointsToBeEarned;
        }

        Assert.Equal(15, totalPointsToBeEarned);
    }

    [Fact]
    public void WhenDiscountIsZeroAndEarnGreaterThanProductMaxPoints_CalculateLoyaltyPoints_ShouldBeReturnCorrectResult()
    {
        decimal totalPointsToBeEarned = 0;
        decimal orderTotalAmount = 150;
        decimal orderPayment = 150;
        decimal productPrice = 150;
        int quantity = 1;
        decimal productPercentageOfPoints = 20;
        decimal productMaxPointsEarned = 16;

        decimal totalDiscount = orderTotalAmount - orderPayment;
        decimal discountPercentage = 100 * totalDiscount / orderTotalAmount;

        if (discountPercentage != 0)
        {
            decimal price = quantity * productPrice;
            decimal priceAfterDiscount = price - (price * discountPercentage / 100);
            decimal pointsToBeEarned = priceAfterDiscount * productPercentageOfPoints / 100;
            decimal totalMaxPoints = productMaxPointsEarned * quantity;

            if (pointsToBeEarned >= totalMaxPoints)
                totalPointsToBeEarned += totalMaxPoints;
            else
                totalPointsToBeEarned += pointsToBeEarned;
        }
        else
        {
            decimal price = quantity * productPrice;
            decimal pointsToBeEarned = price * productPercentageOfPoints / 100;
            decimal totalMaxPoints = productMaxPointsEarned * quantity;

            if (pointsToBeEarned >= totalMaxPoints)
                totalPointsToBeEarned += totalMaxPoints;
            else
                totalPointsToBeEarned += pointsToBeEarned;
        }

        Assert.Equal(16, totalPointsToBeEarned);
    }
}

using WebApi.Data;

namespace WebApi.Test.Methods;

public class CalculatePaymentTest
{
    [Fact]
    public void WhenCouponIsNullAndLoyaltyPointsIsZero_CalculatePayment_ShouldBeReturnCorrectResult()
    {
        decimal totalAmount = 150;
        decimal? payment = totalAmount;
        decimal? couponAmount = null;
        decimal loyaltyPoints = 0;

        if (couponAmount is not null && loyaltyPoints <= 0)
        {
            payment -= couponAmount;

            if (payment < 0)
            {
                payment = 0;
            }
        }

        if (couponAmount is null && loyaltyPoints > 0)
        {
            if (loyaltyPoints >= payment)
            {
                payment = 0;
            }

            payment -= loyaltyPoints;
        }

        if (couponAmount is not null && loyaltyPoints > 0)
        {
            payment -= couponAmount;

            if (payment <= 0)
            {
                payment = 0;
            }

            if (loyaltyPoints >= payment)
            {
                payment = 0;
            }

            payment -= loyaltyPoints;
        }


        Assert.Equal(150, payment);
    }

    [Fact]
    public void WhenCouponIsNotNullAndLoyaltyPointsIsZero_CalculatePayment_ShouldBeReturnCorrectResult()
    {
        decimal totalAmount = 150;
        decimal? payment = totalAmount;
        decimal? couponAmount = 20;
        decimal loyaltyPoints = 0;

        if (couponAmount is not null && loyaltyPoints <= 0)
        {
            payment -= couponAmount;

            if (payment < 0)
            {
                payment = 0;
            }
        }

        if (couponAmount is null && loyaltyPoints > 0)
        {
            if (loyaltyPoints >= payment)
            {
                payment = 0;
            }

            payment -= loyaltyPoints;
        }

        if (couponAmount is not null && loyaltyPoints > 0)
        {
            payment -= couponAmount;

            if (payment <= 0)
            {
                payment = 0;
            }

            if (loyaltyPoints >= payment)
            {
                payment = 0;
            }

            payment -= loyaltyPoints;
        }


        Assert.Equal(130, payment);
    }

    [Fact]
    public void WhenCouponIsNotNullAndLoyaltyPointsIsNotZero_CalculatePayment_ShouldBeReturnCorrectResult()
    {
        decimal totalAmount = 150;
        decimal? payment = totalAmount;
        decimal? couponAmount = 20;
        decimal loyaltyPoints = 10;

        if (couponAmount is not null && loyaltyPoints <= 0)
        {
            payment -= couponAmount;

            if (payment < 0)
            {
                payment = 0;
            }
        }

        if (couponAmount is null && loyaltyPoints > 0)
        {
            if (loyaltyPoints >= payment)
            {
                payment = 0;
            }

            payment -= loyaltyPoints;
        }

        if (couponAmount is not null && loyaltyPoints > 0)
        {
            payment -= couponAmount;

            if (payment <= 0)
            {
                payment = 0;
            }

            if (loyaltyPoints >= payment)
            {
                payment = 0;
            }

            payment -= loyaltyPoints;
        }


        Assert.Equal(120, payment);
    }

    [Fact]
    public void WhenCouponIsNullAndLoyaltyPointsIsNotZero_CalculatePayment_ShouldBeReturnCorrectResult()
    {
        decimal totalAmount = 150;
        decimal? payment = totalAmount;
        decimal? couponAmount = null;
        decimal loyaltyPoints = 10;

        if (couponAmount is not null && loyaltyPoints <= 0)
        {
            payment -= couponAmount;

            if (payment < 0)
            {
                payment = 0;
            }
        }

        if (couponAmount is null && loyaltyPoints > 0)
        {
            if (loyaltyPoints >= payment)
            {
                payment = 0;
            }

            payment -= loyaltyPoints;
        }

        if (couponAmount is not null && loyaltyPoints > 0)
        {
            payment -= couponAmount;

            if (payment <= 0)
            {
                payment = 0;
            }

            if (loyaltyPoints >= payment)
            {
                payment = 0;
            }

            payment -= loyaltyPoints;
        }


        Assert.Equal(140, payment);
    }
}

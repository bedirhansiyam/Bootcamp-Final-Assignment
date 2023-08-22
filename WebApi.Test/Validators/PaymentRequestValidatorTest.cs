using WebApi.Schema;

namespace WebApi.Test.Validators;

public class PaymentRequestValidatorTest
{
    [Fact]
    public void WhenValidInputsAreGiven_PaymentRequestValidator_ErrorCountShouldBeZero()
    {
        PaymentRequest request = new();
        request.CardNumber = "4242 4242 4242 4242";
        request.CardHolder = "cardholdername test";
        request.Cvv = 121;
        request.ExpireYear = 2024;
        request.ExpireMonth = 1;

        PaymentRequestValidator validator = new();
        var result = validator.Validate(request);

        Assert.Equal(0, result.Errors.Count);
    }

    [Theory]
    [InlineData("", "cardholdername test", 121, 2024, 11)]
    [InlineData(null, "cardholdername test", 121, 2024, 11)]
    [InlineData("4242", "cardholdername test", 121, 2024, 11)]
    [InlineData("4242 4242 4242 4242 4242", "cardholdername test", 121, 2024, 11)]
    [InlineData("4242 4242 4242 4242", "", 121, 2024, 11)]
    [InlineData("4242 4242 4242 4242", null, 121, 2024, 11)]
    [InlineData("4242 4242 4242 4242", "c", 121, 2024, 11)]
    [InlineData("4242 4242 4242 4242", "cardholdername testcardholder", 121, 2024, 11)]
    [InlineData("4242 4242 4242 4242", "cardholdername test", null, 2024, 11)]
    [InlineData("4242 4242 4242 4242", "cardholdername test", 15, 2024, 11)]
    [InlineData("4242 4242 4242 4242", "cardholdername test", 4588, 2024, 11)]
    [InlineData("4242 4242 4242 4242", "cardholdername test", 121, null, 11)]
    [InlineData("4242 4242 4242 4242", "cardholdername test", 121, 2015, 11)]
    [InlineData("4242 4242 4242 4242", "cardholdername test", 121, 2065, 11)]
    [InlineData("4242 4242 4242 4242", "cardholdername test", 121, 2024, null)]
    [InlineData("4242 4242 4242 4242", "cardholdername test", 121, 2024, 0)]
    [InlineData("4242 4242 4242 4242", "cardholdername test", 121, 2024, 15)]
    [InlineData("", "", null, null, null)]
    public void WhenInvalidInputsAreGiven_PaymentRequestValidator_ErrorCountShouldBeMoreThanZero(string cardNumber, string cardHolder, int cvv, int expireYear, int expireMonth)
    {
        PaymentRequest request = new();
        request.CardNumber = cardNumber;
        request.CardHolder = cardHolder;
        request.Cvv = cvv;
        request.ExpireYear = expireYear;
        request.ExpireMonth = expireMonth;

        PaymentRequestValidator validator = new();
        var result = validator.Validate(request);

        Assert.NotEqual(0, result.Errors.Count);
    }
}

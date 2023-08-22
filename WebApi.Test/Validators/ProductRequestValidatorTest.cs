using WebApi.Schema;

namespace WebApi.Test.Validators;

public class ProductRequestValidatorTest
{
    [Fact]
    public void WhenValidInputsAreGiven_ProductRequestValidator_ErrorCountShouldBeZero()
    {
        ProductRequest request = new();
        request.Name = "nametest";
        request.Description = "descriptiontest";
        request.ProducerCompany = "producercompanytest";
        request.ReleaseDate = DateTime.Now.AddMonths(-5);
        request.Price = 50;
        request.Stock = 100;
        request.MaxPointsEarned = 10;
        request.PercentageOfPoints = 5;
        request.IsActive = true;


        ProductRequestValidator validator = new();
        var result = validator.Validate(request);

        Assert.Equal(0, result.Errors.Count);
    }

    [Theory]
    [InlineData("", "descriptiontest", "producercompanytest", 50, 100, 10, 5, true)]
    [InlineData(null, "descriptiontest", "producercompanytest", 50, 100, 10, 5, true)]
    [InlineData("na", "descriptiontest", "producercompanytest", 50, 100, 10, 5, true)]
    [InlineData("nametest", "desc", "producercompanytest", 50, 100, 10, 5, true)]
    [InlineData("nametest", "descriptiontest", "pr", 50, 100, 10, 5, true)]
    [InlineData("nametestnametestnametestnametest", "descriptiontest", "producercompanytest", 50, 100, 10, 5, true)]
    [InlineData("nametest", "descriptiontestdescriptiontestdescriptiontestdescriptiontestdescriptiontestdescriptiontestdescriptiontestdescriptiontestdescriptiontestdescriptiontestdescriptiontestdescriptiontestdescriptiontestdescriptiontest", "producercompanytest", 50, 100, 10, 5, true)]
    [InlineData("nametest", "descriptiontest", "producercompanytestproducercompanytestproducercompanytest", 50, 100, 10, 5, true)]
    [InlineData("nametest", "descriptiontest", "producercompanytest", null, 100, 10, 5, true)]
    [InlineData("nametest", "descriptiontest", "producercompanytest", 0, 100, 10, 5, true)]
    [InlineData("nametest", "descriptiontest", "producercompanytest", 50, -10, 10, 5, true)]
    [InlineData("nametest", "descriptiontest", "producercompanytest", 50, 100, null, 5, true)]
    [InlineData("nametest", "descriptiontest", "producercompanytest", 50, 100, -10, 5, true)]
    [InlineData("nametest", "descriptiontest", "producercompanytest", 50, 100, 10, null, true)]
    [InlineData("nametest", "descriptiontest", "producercompanytest", 50, 100, 10, -5, true)]
    [InlineData("nametest", "descriptiontest", "producercompanytest", 50, 100, 10, 205, true)]
    [InlineData("nametest", "descriptiontest", "producercompanytest", 50, 100, 10, 5, null)]
    public void WhenInvalidInputsAreGiven_ProductRequestValidator_ErrorCountShouldBeMoreThanZero(string name, string description, string producerCompany, decimal price, int stock, decimal maxPointsEarned, decimal percentageOfPoints, bool isActive)
    {
        ProductRequest request = new();
        request.Name = name;
        request.Description = description;
        request.ProducerCompany = producerCompany;
        request.ReleaseDate = DateTime.Now.AddMonths(-5);
        request.Price = price;
        request.Stock = stock;
        request.MaxPointsEarned = maxPointsEarned;
        request.PercentageOfPoints = percentageOfPoints;
        request.IsActive = isActive;

        ProductRequestValidator validator = new();
        var result = validator.Validate(request);

        Assert.NotEqual(0, result.Errors.Count);
    }

    [Fact]
    public void WhenInvalidDueDatesAreGiven_CouponRequestValidator_ErrorCountShouldBeMoreThanZero()
    {
        ProductRequest request = new();
        request.Name = "nametest";
        request.Description = "descriptiontest";
        request.ProducerCompany = "producercompanytest";
        request.ReleaseDate = DateTime.Now.AddMonths(5);
        request.Price = 50;
        request.Stock = 100;
        request.MaxPointsEarned = 10;
        request.PercentageOfPoints = 5;
        request.IsActive = true;

        ProductRequestValidator validator = new();
        var result = validator.Validate(request);

        Assert.NotEqual(0, result.Errors.Count);
    }
}

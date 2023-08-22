using WebApi.Schema;

namespace WebApi.Test.Validators;

public class CategoryRequestValidatorTest
{
    [Fact]
    public void WhenValidInputsAreGiven_CategoryRequestValidator_ErrorCountShouldBeZero()
    {
        CategoryRequest request = new();
        request.Name = "nametest";
        request.Tag = "tagtest";
        request.Url = "urltest";

        CategoryRequestValidator validator = new();
        var result = validator.Validate(request);

        Assert.Equal(0, result.Errors.Count);
    }

    [Theory]
    [InlineData("", "tagtest","urltest")]
    [InlineData(null, "tagtest","urltest")]
    [InlineData("n", "tagtest","urltest")]
    [InlineData("nametest", "t","urltest")]
    [InlineData("nametest", "tagtest","u")]
    [InlineData("nametestnametestnametestnametest", "tagtest","urltest")]
    [InlineData("nametest", "tagtesttagtesttagtesttagtesttagtesttagtesttagtesttagtesttagtesttagtesttagtest", "urltest")]
    [InlineData("nametest", "tagtest", "urltesturltesturltesturltesturltest")]
    public void WhenInvalidInputsAreGiven_CategoryRequestValidator_ErrorCountShouldBeMoreThanZero(string name, string tag, string url)
    {
        CategoryRequest request = new();
        request.Name = name;
        request.Tag = tag;
        request.Url = url;

        CategoryRequestValidator validator = new();
        var result = validator.Validate(request);

        Assert.NotEqual(0, result.Errors.Count);
    }
}

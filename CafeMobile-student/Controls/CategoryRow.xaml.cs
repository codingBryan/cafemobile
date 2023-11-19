using CafeMobile.Models;

namespace CafeMobile.Controls;

public partial class CategoryRow : ContentView
{
	public CategoryRow()
	{
		InitializeComponent();
	}
    public static BindableProperty HeadingProperty = BindableProperty.Create(nameof(Heading), typeof(string), typeof(CategoryRow), String.Empty);
    public static BindableProperty CategoriesProperty = BindableProperty.Create(nameof(Categories), typeof(IEnumerable<Category>), typeof(CategoryRow), Enumerable.Empty<Category>());

    public string Heading
    {
        get => (string)GetValue(CategoryRow.HeadingProperty);
        set => SetValue(CategoryRow.HeadingProperty, value);
    }

    public IEnumerable<Category> Categories
    {
        get => (IEnumerable<Category>)GetValue(CategoryRow.CategoriesProperty);
        set => SetValue(CategoryRow.CategoriesProperty, value);
    }
}
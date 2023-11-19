using CafeMobile.ViewModels.StudentVms;

namespace CafeMobile.Pages.Student;

public partial class StudentLogin : ContentPage
{
	public StudentLogin()
	{
		InitializeComponent();
		BindingContext = new StudentLoginViewModel();
	}
}
using CafeMobile.ViewModels.StudentVms;

namespace CafeMobile.Pages.Student;

public partial class StudentSignUp : ContentPage
{
    public StudentSignUp(StudentSignUpViewModel viewmodel)
	{
        InitializeComponent();
        BindingContext = viewmodel;
	}
}
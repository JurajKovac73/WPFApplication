using MvvmHelpers;

namespace ServiceHub.ViewModels
{
    public class ViewModelBase : BaseViewModel
    {
        private string? _errorMessage;
        public string? ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }
    }
}

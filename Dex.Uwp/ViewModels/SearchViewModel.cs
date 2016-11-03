using Dex.Core.Repositories;
using Dex.Uwp.Infrastructure;
using Dex.Uwp.Services;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace Dex.Uwp.ViewModels
{
    public class SearchViewModel : ViewModelBase
    {
        private readonly IMoveRepository moveRepository;
        private readonly INavigationService navigationService;
        private readonly IPokemonRepository pokemonsRepository;

        public SearchViewModel(IPokemonRepository pokemonsRepository, IMoveRepository moveRepository, INavigationService navigationService)
        {
            this.navigationService = navigationService;
            this.moveRepository = moveRepository;
            this.pokemonsRepository = pokemonsRepository;
        }

        public override Task OnNavigatedTo(NavigationEventArgs e)
        {
            return base.OnNavigatedTo(e);
        }
    }
}
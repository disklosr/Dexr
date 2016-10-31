using Dex.Core.Entities;
using Dex.Core.Repositories;
using Dex.Uwp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Navigation;

namespace Dex.Uwp.ViewModels
{
    public class MovedexViewModel : ViewModelBase
    {
        private readonly IMoveRepository moveRepository;
        private PokemonMoves allMoves;

        public MovedexViewModel(IMoveRepository moveRepository)
        {
            this.moveRepository = moveRepository;
            ReverseOrderCommand = new RelayCommand(() => OnReverseOrder());
        }

        public IEnumerable<ChargeMove> AllChargeMovesById { get; private set; }
        public IEnumerable<QuickMove> AllQuickMovesById { get; private set; }
        public ICommand ReverseOrderCommand { get; }

        public async override Task OnNavigatedTo(NavigationEventArgs e)
        {
            allMoves = await moveRepository.GetAllMoves();
            AllChargeMovesById = allMoves.ChargeMoves;
            AllQuickMovesById = allMoves.QuickMoves;
        }

        private void OnReverseOrder()
        {
            AllChargeMovesById = AllChargeMovesById.Reverse();
            AllQuickMovesById = AllQuickMovesById.Reverse();
        }
    }
}
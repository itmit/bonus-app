using System;
using System.Linq;
using System.Threading.Tasks;
using bonus.app.Core.Dtos.GeoHelper;
using bonus.app.Core.Models;
using bonus.app.Core.Services;
using MvvmCross.ViewModels;
using Realms;

namespace bonus.app.Core.ViewModels.Businessman.Shares
{
	public class EditorStockViewModel : MvxViewModel<Stock>
	{
		private readonly IGeoHelperService _geoHelperService;
		private Stock _stock;
		private MvxObservableCollection<Country> _countries;
		private Country _selectedCountry;

		public EditorStockViewModel(IGeoHelperService geoHelperService)
		{
			_geoHelperService = geoHelperService;
		}

		public Stock Stock
		{
			get => _stock;
			private set => SetProperty(ref _stock, value);
		}

		public Country SelectedCountry
		{
			get => _selectedCountry;
			set => SetProperty(ref _selectedCountry, value);
		}
		public override void Prepare(Stock parameter)
		{
			Stock = parameter;
		}

		public override async Task Initialize()
		{
			await base.Initialize();

			try
			{
				var countries = await _geoHelperService.GetCountries(new LocaleDto
				{
					FallbackLang = "en",
					Lang = "ru"
				});
				countries.Move(countries.Single(c => c.Iso.Equals("RU")), 0);
				countries.Move(countries.Single(c => c.Iso.Equals("UA")), 1);
				countries.Move(countries.Single(c => c.Iso.Equals("BY")), 2);
				countries.Move(countries.Single(c => c.Iso.Equals("KZ")), 3);
				countries.Move(countries.Single(c => c.Iso.Equals("AZ")), 4);
				Countries = new MvxObservableCollection<Country>(countries.Where(c => !string.IsNullOrEmpty(c.LocalizedNames.Ru)));
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		public MvxObservableCollection<Country> Countries
		{
			get => _countries;
			private set => SetProperty(ref _countries, value);
		}
	}
}

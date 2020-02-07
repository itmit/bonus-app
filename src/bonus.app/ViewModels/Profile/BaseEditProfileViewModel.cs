using System;
using System.Linq;
using System.Threading.Tasks;
using bonus.app.Core.Dto.GeoHelper;
using bonus.app.Core.Models;
using bonus.app.Core.Repositories;
using bonus.app.Core.Services;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Profile
{
	public abstract class BaseEditProfileViewModel : MvxViewModel

	{
	private bool _isAuthorization;

	public bool IsAuthorization
	{
		get => _isAuthorization;
		private set => SetProperty(ref _isAuthorization, value);
	}

	private readonly IUserRepository _userRepository;
	private readonly IMvxNavigationService _navigationService;
	private User _user;
	private readonly IGeoHelperService _geoHelperService;
	private MvxObservableCollection<Country> _countries;
	private Country _selectedCountry;

	public BaseEditProfileViewModel(IUserRepository userRepository, IMvxNavigationService navigationService, IGeoHelperService geoHelperService)
	{
		_userRepository = userRepository;
		_navigationService = navigationService;
		_geoHelperService = geoHelperService;
	}

	public MvxObservableCollection<Country> Countries
	{
		get => _countries;
		private set => SetProperty(ref _countries, value);
	}

	public override async Task Initialize()
	{
		await base.Initialize();

		User = _userRepository.GetAll()
							  .SingleOrDefault();
		IsAuthorization = User != null;

		try
		{
			Countries = new MvxObservableCollection<Country>(await _geoHelperService.GetCountries(new LocaleDto
			{
				FallbackLang = "en",
				Lang = "ru"
			}));
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
		}
	}

	public Country SelectedCountry
	{
		get => _selectedCountry;
		set => SetProperty(ref _selectedCountry, value);
	}

	public User User
	{
		get => _user;
		private set => SetProperty(ref _user, value);
	}

	}
}

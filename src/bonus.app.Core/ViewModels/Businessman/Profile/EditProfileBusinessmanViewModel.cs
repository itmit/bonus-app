using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using bonus.app.Core.Dtos.BusinessmanDtos;
using bonus.app.Core.Models;
using bonus.app.Core.Models.UserModels;
using bonus.app.Core.Services;
using bonus.app.Core.Validations;
using bonus.app.Core.ViewModels.Auth;
using bonus.app.Core.ViewModels.Businessman.Services;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace bonus.app.Core.ViewModels.Businessman.Profile
{
	public class EditProfileBusinessmanViewModel : BaseEditProfileViewModel, IPortfolioParentViewModel
	{
		#region Data
		#region Fields
		private MvxCommand _addPortfolioImageCommand;
		private ValidatableObject<string> _classmatesLink = new ValidatableObject<string>
		{
			Value = string.Empty
		};
		private ValidatableObject<string> _contact = new ValidatableObject<string>
		{
			Value = string.Empty
		};
		private string _description = string.Empty;
		private MvxCommand _editCommand;
		private ValidatableObject<string> _email = new ValidatableObject<string>
		{
			Value = string.Empty
		};
		private ValidatableObject<string> _facebookLink = new ValidatableObject<string>
		{
			Value = string.Empty
		};
		private ValidatableObject<string> _instagrammLink = new ValidatableObject<string>
		{
			Value = string.Empty
		};
		private readonly Mapper _mapper;
		private ValidatableObject<string> _name = new ValidatableObject<string>
		{
			Value = string.Empty
		};
		private readonly IMvxNavigationService _navigationService;
		private MvxObservableCollection<PortfolioViewModel> _portfolioImages = new MvxObservableCollection<PortfolioViewModel>();
		private readonly IProfileService _profileService;
		private ValidatableObject<string> _vkLink = new ValidatableObject<string>
		{
			Value = string.Empty
		};
		private ValidatableObject<string> _workingMode = new ValidatableObject<string>
		{
			Value = string.Empty
		};
		#endregion
		#endregion

		#region .ctor
		public EditProfileBusinessmanViewModel(IAuthService authService,
											   IMvxNavigationService navigationService,
											   IGeoHelperService geoHelperService,
											   IProfileService profileService,
											   IPermissionsService permissionsService)
			: base(authService, geoHelperService, permissionsService)
		{
			_navigationService = navigationService;
			_profileService = profileService;
			_mapper = new Mapper(new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<PortfolioImage, PortfolioViewModel>()
				   .ConstructUsing(m => new PortfolioViewModel(profileService)
				   {
					   ImageSource = m.ImageSource,
					   ImageName = m.ImageName,
					   ParentViewModel = this,
					   PortfolioImage = m
				   });
			}));

			AddValidations();
		}
		#endregion

		#region Properties
		public CreateServiceViewModel CreateServiceViewModel
		{
			get;
			private set;
		}

		public MvxCommand AddPortfolioImageCommand
		{
			get
			{
				_addPortfolioImageCommand = _addPortfolioImageCommand ??
											new MvxCommand(async () =>
											{
												if (!await PermissionsService.RequestPermissionAsync<StoragePermission>(Permission.Storage,
																									 "Для загрузки аватара необходимо разрешение на использование хранилища."))
												{
													return;
												}

												if (!CrossMedia.Current.IsPickPhotoSupported)
												{
													return;
												}

												var image = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
												{
													PhotoSize = PhotoSize.Medium
												});

												if (image == null)
												{
													return;
												}

												var portfolioImage = await _profileService.AddImageToPortfolio(image.Path);

												if (portfolioImage == null)
												{
													return;
												}

												PortfolioImages.Add(_mapper.Map<PortfolioViewModel>(portfolioImage));
												await RaisePropertyChanged(() => PortfolioImages);
											});
				return _addPortfolioImageCommand;
			}
		}

		public ValidatableObject<string> ClassmatesLink
		{
			get => _classmatesLink;
			set => SetProperty(ref _classmatesLink, value);
		}

		public ValidatableObject<string> Contact
		{
			get => _contact;
			set => SetProperty(ref _contact, value);
		}

		public string Description
		{
			get => _description;
			set => SetProperty(ref _description, value);
		}

		public MvxCommand EditCommand
		{
			get
			{
				_editCommand = _editCommand ?? new MvxCommand(EditCommandExecute);
				return _editCommand;
			}
		}

		public ValidatableObject<string> Email
		{
			get => _email;
			set => SetProperty(ref _email, value);
		}

		public ValidatableObject<string> FacebookLink
		{
			get => _facebookLink;
			set => SetProperty(ref _facebookLink, value);
		}

		public ValidatableObject<string> InstagramLink
		{
			get => _instagrammLink;
			set => SetProperty(ref _instagrammLink, value);
		}

		public ValidatableObject<string> Name
		{
			get => _name;
			set => SetProperty(ref _name, value);
		}

		public MvxObservableCollection<PortfolioViewModel> PortfolioImages
		{
			get => _portfolioImages;
			set => SetProperty(ref _portfolioImages, value);
		}

		public ValidatableObject<string> VkLink
		{
			get => _vkLink;
			set => SetProperty(ref _vkLink, value);
		}

		public ValidatableObject<string> WorkingMode
		{
			get => _workingMode;
			set => SetProperty(ref _workingMode, value);
		}
		#endregion

		#region IPortfolioParentViewModel members
		public void RemovedPortfolioImage(PortfolioViewModel portfolioViewModel)
		{
			PortfolioImages.Remove(portfolioViewModel);
			RaisePropertyChanged(() => PortfolioImages);
		}
		#endregion

		#region Overrided
		public override async Task Initialize()
		{
			await base.Initialize();

			if (Parameters.IsActiveUser && User != null)
			{
				await CreateServiceViewModel.Initialize();
				WorkingMode.Value = User.WorkTime;
				Contact.Value = User.Contact;
				Address.Value = User.Address;
				Description = User.Description;
				Email.Value = User.Email;
				Name.Value = User.Name;
				VkLink.Value = User.VkLink;
				InstagramLink.Value = User.InstagramLink;
				FacebookLink.Value = User.FacebookLink;
				ClassmatesLink.Value = User.ClassmatesLink;

				try
				{
					PortfolioImages = new MvxObservableCollection<PortfolioViewModel>(_mapper.Map<List<PortfolioViewModel>>(await _profileService.GetPortfolio()));
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}
		}

		private ValidatableObject<string> _address = new ValidatableObject<string>();

		public ValidatableObject<string> Address
		{
			get => _address;
			set => SetProperty(ref _address, value);
		}

		public override void Prepare(EditProfileViewModelArguments parameter)
		{
			base.Prepare(parameter);
			if (!Parameters.IsActiveUser)
			{
				return;
			}

			CreateServiceViewModel = new CreateServiceViewModel(Mvx.IoCProvider.Resolve<IServicesService>(), AuthService);

			Email.Validations.Add(new IsNotNullOrEmptyRule
			{
				ValidationMessage = "Укажите Email адрес."
			});
			Email.Validations.Add(new IsValidEmailRule
			{
				ValidationMessage = "Не корректно введен Email."
			});
			Name.Validations.Add(new IsNotNullOrEmptyRule
			{
				ValidationMessage = "Укажите торговое название или имя мастера."
			});
			Name.Validations.Add(new MinLengthRule(2)
			{
				ValidationMessage = "Торговое название или имя мастера не может быть меньше 2 символов."
			});
			VkLink.Validations.Add(new IsValidUriRule
			{
				ValidationMessage = "Не корректная ссылка."
			});
			VkLink.Validations.Add(new IsSuccessRegexMatch(
									   new Regex(@"(^(https:\/\/)|^(http:\/\/))+vk\.com\/+.{5,32}", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline))
			{
				ValidationMessage = "Введенная ссылка не является ссылкой на vk."
			});
			InstagramLink.Validations.Add(new IsValidUriRule
			{
				ValidationMessage = "Не корректная ссылка."
			});
			InstagramLink.Validations.Add(new IsSuccessRegexMatch(
											  new Regex(@"(^(https:\/\/)|^(http:\/\/))+((www\.)?)+instagram\.com\/+(.{1,30})+\?.", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline))
										  {
											  ValidationMessage = "Введенная ссылка не является ссылкой на instagram."
			});
			FacebookLink.Validations.Add(new IsValidUriRule
			{
				ValidationMessage = "Не корректная ссылка."
			});
			FacebookLink.Validations.Add(new IsSuccessRegexMatch(
											 new Regex(@"(^(https:\/\/)|^(http:\/\/))+((www\.)?)+facebook\.com\/+.{5,64}", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline))
										 {
											 ValidationMessage = "Введенная ссылка не является ссылкой на Facebook."
			});
			ClassmatesLink.Validations.Add(new IsValidUriRule
			{
				ValidationMessage = "Не корректная ссылка."
			});

			ClassmatesLink.Validations.Add(new IsSuccessRegexMatch(
											   new Regex(@"(^(https:\/\/)|^(http:\/\/))+ok\.ru\/+.{1,64}", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline))
										   {
											   ValidationMessage = "Введенная ссылка не является ссылкой на одноклассники."
										   });
		}
		#endregion

		#region Private
		private void AddValidations()
		{
			WorkingMode.Validations.Add(new IsNotNullOrEmptyRule
			{
				ValidationMessage = "Укажите режим работы."
			});
			Contact.Validations.Add(new IsNotNullOrEmptyRule
			{
				ValidationMessage = "Укажите контактное лицо."
			});
			Address.Validations.Add(new IsNotNullOrEmptyRule
			{
				ValidationMessage = "Укажите адрес."
			});
			Address.Validations.Add(new MinLengthRule(6)
			{
				ValidationMessage = "Адрес не может быть меньше 6 символов."
			});
			PhoneNumber.Validations.Add(new IsNotNullOrEmptyRule
			{
				ValidationMessage = "Укажите номер телефона."
			});
			PhoneNumber.Validations.Add(new IsValidPhoneNumberRule
			{
				ValidationMessage = "Не корректный номер телефона."
			});
		}

		private async void EditCommandExecute()
		{
			if (!Validate())
			{
				return;
			}

			var loading = await MaterialDialog.Instance.LoadingDialogAsync(message: "Сохранение данных...");
			try
			{
				var arg = new EditBusinessmanDto
				{
					Uuid = Parameters.Guid,
					Country = CountryAndCityViewModel.SelectedCountry.LocalizedNames.Ru,
					City = CountryAndCityViewModel.SelectedCity.LocalizedNames.Ru,
					Address = Address.Value,
					WorkTime = WorkingMode.Value,
					Contact = Contact.Value,
					Description = Description,
					Phone = PhoneNumber.Value,
					Password = Parameters.Password
				};

				if (User != null && Parameters.IsActiveUser)
				{
					var p = Regex.Replace(PhoneNumber.Value, "[@,\\ \\(\\)\\-]", string.Empty);
					if (User.Phone.Equals(p))
					{
						arg.Phone = string.Empty;
					}

					if (User.Email.Equals(Email.Value))
					{
						arg.Email = string.Empty;
					}

					arg.Name = Name.Value;
					arg.VkLink = VkLink.Value;
					arg.InstagramLink = InstagramLink.Value;
					arg.FacebookLink = FacebookLink.Value;
					arg.Odnoklassniki = ClassmatesLink.Value;
				}

				var user = await _profileService.Edit(arg, ImageSource);

				if (Parameters.IsActiveUser)
				{
					await loading.DismissAsync();
					await MaterialDialog.Instance.AlertAsync("Изменения сохранены успешно.", "Внимание", "Ок");
					await _navigationService.Close(this, user);
					return;
				}

				if (user?.AccessToken != null && !string.IsNullOrEmpty(user.AccessToken.Body))
				{
					await loading.DismissAsync();
					await _navigationService.Navigate<SuccessRegisterBusinessmanPopupViewModel>();
					await _navigationService.Navigate<MainBusinessmanViewModel>();
					return;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			await loading.DismissAsync();

			if (_profileService.ErrorDetails != null && _profileService.ErrorDetails.Count > 0)
			{
				var key = _profileService.ErrorDetails.First()
										 .Key;
				if (key.Equals("phone"))
				{
					await MaterialDialog.Instance.AlertAsync("Пользователь с таким номером уже существует.", "Ошибка", "Ок");
					return;
				}
			}

			if (!string.IsNullOrEmpty(_profileService.Error))
			{
				await MaterialDialog.Instance.AlertAsync(_profileService.Error, "Ошибка", "Ок");
			}
		}

		public delegate void EditErrorHandler(string[] propertyName);

		public EditErrorHandler FailedEdit;

		private bool Validate()
		{
			var isValid = true;
			var props = new List<string>();

			if (!WorkingMode.Validate())
			{
				isValid = false;
				props.Add(nameof(WorkingMode));
			}
			
			if (!Contact.Validate())
			{
				isValid = false;
				props.Add(nameof(Contact));
			}
			
			if (!Address.Validate())
			{
				isValid = false;
				props.Add(nameof(Address));
			}

			if (!PhoneNumber.Validate())
			{
				isValid = false;
				props.Add(nameof(PhoneNumber));
			}

			if (CountryAndCityViewModel.SelectedCountry == null)
			{
				MaterialDialog.Instance.AlertAsync("Выберите страну.", "Внимание", "Ок");
				return false;
			}

			if (CountryAndCityViewModel.SelectedCity == null)
			{
				MaterialDialog.Instance.AlertAsync("Выберите город.", "Внимание", "Ок");
				return false;
			}

			if (!Parameters.IsActiveUser)
			{
				if (!isValid)
				{
					FailedEdit?.Invoke(props.ToArray());
				}
				return isValid;
			}

			if (!string.IsNullOrEmpty(VkLink.Value))
			{
				if (!VkLink.Validate())
				{
					isValid = false;
					props.Add(nameof(VkLink));
				}
			}

			if (!string.IsNullOrEmpty(InstagramLink.Value))
			{
				if (!InstagramLink.Validate())
				{
					isValid = false;
					props.Add(nameof(InstagramLink));
				}
			}

			if (!string.IsNullOrEmpty(FacebookLink.Value))
			{
				if (!FacebookLink.Validate())
				{
					isValid = false;
					props.Add(nameof(FacebookLink));
				}
			}

			if (!string.IsNullOrEmpty(ClassmatesLink.Value))
			{
				if (!ClassmatesLink.Validate())
				{
					isValid = false;
					props.Add(nameof(ClassmatesLink));
				}
			}

			if (!Email.Validate())
			{
				isValid = false;
				props.Add(nameof(EditCommand));
			}

			if (!Name.Validate())
			{
				isValid = false;
				props.Add(nameof(Name));
			}
			if (!isValid)
			{
				FailedEdit?.Invoke(props.ToArray());
			}
			return isValid;
		}
		#endregion
	}
}

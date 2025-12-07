using System;
using Umbraco.Extensions;
using Umbraco.Cms.Core.Web;
using System.Threading.Tasks;
using System.Collections.Generic;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Api.Management.ViewModels;
using Umbraco.Cms.Api.Management.Services.Flags;
using Umbraco.Cms.Api.Management.ViewModels.Tree;
using Umbraco.Cms.Api.Management.ViewModels.Document.Item;
using Umbraco.Cms.Api.Management.ViewModels.Document.Collection;

namespace UmbracoNaviHideIcon
{
	internal class UmbracoNaviHideIconFlagProvider : IFlagProvider
	{
		private readonly IUmbracoContextFactory _umbracoContextFactory;

		public UmbracoNaviHideIconFlagProvider(IUmbracoContextFactory umbracoContextFactory)
		{
			_umbracoContextFactory = umbracoContextFactory;
		}

		public bool CanProvideFlags<TItem>()
			where TItem : IHasFlags =>
			typeof(TItem) == typeof(DocumentTreeItemResponseModel) ||
			typeof(TItem) == typeof(DocumentCollectionResponseModel) ||
				typeof(TItem) == typeof(DocumentItemResponseModel);

		public Task PopulateFlagsAsync<TItem>(IEnumerable<TItem> itemViewModels)
			where TItem : IHasFlags
		{
			using var umbracoContextReference = _umbracoContextFactory.EnsureUmbracoContext();

			foreach (TItem item in itemViewModels)
			{
				if (ShouldAddFlag(item, umbracoContextReference.UmbracoContext))
					item.AddFlag("UmbracoNaviHideIconFlag");
			}

			return Task.CompletedTask;
		}

		private bool ShouldAddFlag<TItem>(TItem item, IUmbracoContext umbracoContext)
		{
			try
			{
				Guid itemId = Guid.Empty;
				switch (item)
				{
					case DocumentTreeItemResponseModel treeItem:
						itemId = treeItem.Id;
						break;
					case DocumentCollectionResponseModel collectionItem:
						itemId = collectionItem.Id;
						break;

					case DocumentItemResponseModel documentItem:
						itemId = documentItem.Id;
						break;

					default:
						return false;
				}

				if (itemId == Guid.Empty)
					return false;

				var content = umbracoContext.Content?.GetById(itemId);
				if (content == null)
					return false;

				var naviHideValue = content.Value<bool>("umbracoNaviHide");
				return naviHideValue;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
	}
	internal class UmbracoNaviHideIconComposer : IComposer
	{
		public void Compose(IUmbracoBuilder builder)
		{
			builder.FlagProviders().Append<UmbracoNaviHideIconFlagProvider>();
		}
	}
}
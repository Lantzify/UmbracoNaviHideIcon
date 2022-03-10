using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;

namespace UmbracoNaviHideIcon
{
	public class ContentTreeNodesRenderingNotification : INotificationHandler<TreeNodesRenderingNotification>
	{
		private readonly IUmbracoContextFactory _umbracoContextFactory;
		public ContentTreeNodesRenderingNotification(IUmbracoContextFactory umbracoContextFactory)
		{
			_umbracoContextFactory = umbracoContextFactory;
		}
		public void Handle(TreeNodesRenderingNotification notification)
		{
			if (notification.TreeAlias.Equals("content"))
			{
				foreach (var node in notification.Nodes)
				{
					using (UmbracoContextReference umbracoContextReference = _umbracoContextFactory.EnsureUmbracoContext())
					{
						var umbracoNode = umbracoContextReference.UmbracoContext.Content.GetById(int.Parse(node.Id.ToString()));
						if (umbracoNode != null && umbracoNode.HasValue("umbracoNaviHide") && umbracoNode.Value<bool>("umbracoNaviHide"))
						{
							node.CssClasses.Add("umbracoNaviHideIcon");
						}
					}
					
				}
			}
		}
	}
}

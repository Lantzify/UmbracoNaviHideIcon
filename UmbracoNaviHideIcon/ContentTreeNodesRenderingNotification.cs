#if NETCOREAPP
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;
#else
using Umbraco.Web;
using Umbraco.Web.Trees;
#endif



namespace UmbracoNaviHideIcon
{
#if NETCOREAPP
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
						if (umbracoNode != null && !umbracoNode.IsVisible())
						{
							node.CssClasses.Add("umbracoNaviHideIcon");
						}
					}
					
				}
			}
		}
	}

#else
	public static class ContentTreeNodesRenderingNotification
	{
		public static void TreeControllerBase_TreeNodesRendering(TreeControllerBase sender, TreeNodesRenderingEventArgs e)
		{
			if (sender.TreeAlias.Equals("content"))
			{
				foreach (var node in e.Nodes)
				{
					var umbNode = sender.UmbracoContext.Content.GetById(int.Parse(node.Id.ToString()));
					if (umbNode != null && !umbNode.IsVisible())
					{
						node.CssClasses.Add("umbracoNaviHideIcon");
						node.CssClasses.Add("v8");
					}
				}
			}
		}
	}
#endif
}
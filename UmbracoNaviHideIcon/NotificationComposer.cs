#if NETCOREAPP
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.DependencyInjection;
#else
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Web.Trees;
using static UmbracoNaviHideIcon.ContentTreeNodesRenderingNotification;
#endif



namespace UmbracoNaviHideIcon
{
#if NETCOREAPP
	public class NotificationComposer : IComposer
	{
		public void Compose(IUmbracoBuilder builder)
		{
			builder.AddNotificationHandler<TreeNodesRenderingNotification, ContentTreeNodesRenderingNotification>();
		}
	}
#else
	[RuntimeLevel(MinLevel = RuntimeLevel.Run)]
	public class NotificationComposer : IUserComposer
	{
		public void Compose(Composition composition)
		{
			composition.Components().Append<NotificationComponent>();
		}
	}

	public class NotificationComponent : IComponent
	{
		public void Initialize()
		{
			TreeControllerBase.TreeNodesRendering += TreeControllerBase_TreeNodesRendering;
		}

		public void Terminate()
		{
			TreeControllerBase.TreeNodesRendering -= TreeControllerBase_TreeNodesRendering;
		}
	}
#endif
	}

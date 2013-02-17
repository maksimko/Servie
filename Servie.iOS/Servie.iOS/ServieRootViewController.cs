using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using Servie.Client;
using Servie.Domain;
using System.Net;

namespace Servie.iOS
{
	public partial class ServieRootViewController : UIViewController
	{
		UIButton _requestButton;
		UILabel _responseLabel;

		public ServieRootViewController()
		{
			InitController();
			ApplyStyles();

			Subscribe ();
		}

		void InitController()
		{
			_requestButton = UIButton.FromType (UIButtonType.RoundedRect);
			_requestButton.SetTitle ("Request", UIControlState.Normal);
			_requestButton.SizeToFit ();

			_requestButton.TouchUpInside += OnRequestButtonTouched;

			_responseLabel = new UILabel();
			_responseLabel.Font = UIFont.FromName("TamilSangamMN", 10);

			View.Add (_requestButton);
			View.Add (_responseLabel);
		}

		void ApplyStyles()
		{
			View.BackgroundColor = UIColor.White;
		}

		void Subscribe()
		{
			ConnectionManager.Instance.ErrorOccurred += OnConnectionError;
			ConnectionManager.Authentificator.LoginSucceed += OnLogin;
		}

		void Unsubscribe()
		{
			ConnectionManager.Instance.ErrorOccurred -= OnConnectionError;
			ConnectionManager.Authentificator.LoginSucceed -= OnLogin;
		}

		void OnConnectionError (Exception ex, HttpStatusCode code)
		{
			_responseLabel.Text = ex.Message;
			_responseLabel.SizeToFit();
		}

		void OnLogin (Person person)
		{
			_responseLabel.Text = String.Format ("Response: login - {0} | password - {1}", person.Login, person.Password);
			_responseLabel.SizeToFit();
		}

		void OnRequestButtonTouched (object sender, EventArgs e)
		{
			ConnectionManager.Authentificator.Login ();
		}

		public override void ViewDidLayoutSubviews ()
		{
			_requestButton.Center = new PointF (View.Bounds.GetMidX(), View.Bounds.GetMidY());
			_responseLabel.Center = new PointF (_requestButton.Center.X, _requestButton.Center.Y + 40);
		}

		protected override void Dispose (bool disposing)
		{
			base.Dispose (disposing);

			Unsubscribe();

			_requestButton.RemoveFromSuperview();
			_requestButton.Dispose();

			_responseLabel.RemoveFromSuperview();
			_responseLabel.Dispose();
		}
	}
}


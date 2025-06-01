# InnovatieProject - Campaign Click Tracking

This project is a Razor Pages web application (ASP.NET Core, .NET 8) that allows you to create email campaigns, send personalized tracking links, and monitor the results of link clicks.

## Features

- **Create Campaigns:**  
  Easily create a campaign, compose the email content, and send test emails to one or more recipients. Each recipient receives a unique tracking link.

- **Click Tracking:**  
  When a recipient clicks the tracking link, the click is registered with campaign and user information.

- **View Results:**  
  See an overview of all registered clicks, including campaign, user, and timestamp.

## Key Pages

- `/Create`  
  Form to create a campaign and send test emails. Generates a unique tracking link for each recipient.

- `/Track`  
  Accessed via the tracking link in the email. Registers the click and shows a thank-you page.

- `/Results`  
  Displays a table with all registered clicks.

## Installation & Usage

1. **Requirements**
   - .NET 8 SDK
   - Visual Studio 2022 or newer
   - **Papercut SMTP** (for local email testing)

  > You can download Papercut SMTP from:  
  > [https://github.com/ChangemakerStudios/Papercut-SMTP](https://github.com/ChangemakerStudios/Papercut-SMTP)


2. **Start the project**
```bash
dotnet restore
dotnet run
```

Then open the application in your browser at the displayed URL (usually `https://localhost:5001`).

3. **Send Emails**
   - Go to the `/Create` page and fill in the campaign details.
   - Add email addresses (one per line).
   - Send the test emails. Each recipient will receive a unique tracking link.

4. **Track Clicks**
   - When a recipient clicks the link, the click is immediately registered.

5. **View Results**
   - Go to `/Results` for an overview of all clicks.

## Technical Details

- **Click Logging:**  
  Clicks are stored in-memory using a thread-safe list (`ConcurrentBag`). See `Services/ClickTracker.cs`.

- **Tracking Links:**  
  Each recipient gets a unique link containing campaign and user information (base64-encoded).

- **Email Sending:**  
  Emails are sent using MailKit via a local SMTP server.  
  During development, [Papercut SMTP](https://github.com/ChangemakerStudios/Papercut-SMTP) is used to capture and view test emails without sending them externally.

## Notes

- Click logs are stored in memory only and will be lost when the application restarts.
- For production use, consider persisting click logs (e.g., in a database) and updating SMTP settings.

## File Structure

- `Pages/Create.cshtml` & `Create.cshtml.cs` — Create campaigns and send emails
- `Pages/Track.cshtml` & `Track.cshtml.cs` — Register clicks via tracking link
- `Pages/Results.cshtml` & `Results.cshtml.cs` — Overview of all clicks
- `Services/ClickTracker.cs` — Click tracking logic

## License

This project is intended for educational purposes.

---

Questions or suggestions? Please contact the maintainer or open an issue!

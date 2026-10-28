# inventory-tracker

## Admin user invitations

Administrators can invite users from **Invite users** in the application header.
Each invitation assigns one of the configured roles and expires after seven
days. The recipient follows the emailed link to choose their name and password;
the link is single-use.

Configure `UIUrl`, `Email:FromAddress`, and `Email:Smtp` settings for the
frontend URL and SMTP server. Keep SMTP credentials in environment variables or
a secret store rather than committing them to configuration files.

The `Admin` and `User` roles are seeded by the API.
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstractions.Persistence;

public interface IEmailService
{
    Task SendEmailAsync( string to, string subject,  string body,
        CancellationToken cancellationToken = default);
}


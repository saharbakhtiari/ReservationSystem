using Application_Frontend.Common;
using Extensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Frontend.Menus.Queries.GetMenues;

public class GetMenuesQuery : IRequest<Menu>
{
}

public class GetMenuesQueryHandler : IRequestHandler<GetMenuesQuery, Menu>
{
    private readonly IAuthService _authService;

    public GetMenuesQueryHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<Menu> Handle(GetMenuesQuery request, CancellationToken cancellationToken)
    {
        var menu = MenuProvider.Menu;
        await CleanNotPermissionMenu(menu.Items);
        return menu;
    }
    private async Task CleanNotPermissionMenu(List<MenuItem> menuItems)
    {
        if (menuItems != null)
        {
            for (int i = 0; i < menuItems.Count; i++)
            {
                if (!(await _authService.HasPermissionOrRole(menuItems[i].Permission, menuItems[i].Role)))
                {
                    Console.WriteLine($"user has not role {menuItems[i].Role}");
                    menuItems.RemoveAt(i);
                    i--;
                    continue;
                }
                if (menuItems[i].Items.IsNullOrEmpty().Not())
                {
                    await CleanNotPermissionMenu(menuItems[i].Items);
                    if (menuItems[i].Items.IsNullOrEmpty())
                    {
                        menuItems.RemoveAt(i);
                        i--;
                    }
                }
            }
        }
    }
}

using Relax.Properties;
using System;
using System.Windows.Forms;

public class MainForm : Form
{
    private readonly ContextMenu contextMenu;
    private readonly System.ComponentModel.IContainer components;
    private readonly string[] menus = { "开启提醒", "提醒频率", "退出" };
    private readonly string[] subMenus = { "30分钟", "1小时", "2小时" };
    private bool isRemain = true; // 默认打开提醒
    private int remainIndex = 1; // 默认提醒时间为1小时

    public NotifyIcon NotifyIcon { get; set; }

    [STAThread]
    static void Main()
    {
        Application.Run(new MainForm());
    }

    public MainForm()
    {
        components = new System.ComponentModel.Container();
        contextMenu = new ContextMenu();
        InitNotifyIcon();
        InitMenus();
    }

    private void InitNotifyIcon()
    {
        NotifyIcon = new NotifyIcon(this.components)
        {
            Icon = Resources.relax
        };
        NotifyIcon.ContextMenu = contextMenu;
        NotifyIcon.Visible = true;
    }

    private void InitMenus()
    {
        for (int i = 0; i < menus.Length; i++)
        {
            MenuItem menuItem = new MenuItem
            {
                Index = i,
                Text = menus[i]
            };
            menuItem.Click += MenuItem_Click;
            if (i == 0)
            {
                menuItem.Checked = isRemain;
            }
            if (i == 1)
            {
                for (int j = 0; j < subMenus.Length; j++)
                {
                    MenuItem subMenuItem = new MenuItem
                    {
                        Index = j,
                        Text = subMenus[j]
                    };
                    subMenuItem.Click += SubMenuItem_Click;
                    subMenuItem.Checked = j == remainIndex;
                    menuItem.MenuItems.Add(subMenuItem);
                }
            }
            contextMenu.MenuItems.Add(menuItem);
        }
    }

    private void SubMenuItem_Click(Object sender, EventArgs e)
    {
        MenuItem subMenuItem = (MenuItem)sender;
        remainIndex = Array.IndexOf(subMenus, subMenuItem.Text);
        foreach (MenuItem item in contextMenu.MenuItems[1].MenuItems)
        {
            item.Checked = Array.IndexOf(subMenus,item.Text) == remainIndex;
        }
    }

    private void MenuItem_Click(Object sender, EventArgs e)
    {
        MenuItem item = (MenuItem)sender;
        if ("开启提醒".Equals(item.Text))
        {
            isRemain = !isRemain;
            item.Checked = isRemain;
        }
    }
}
let Player = mp.players.local;
const NativeUI = require("Menus/Nativeui");
const Menu = NativeUI.Menu;
const UIMenuItem = NativeUI.UIMenuItem;
const UIMenuListItem = NativeUI.UIMenuListItem;
const UIMenuCheckboxItem = NativeUI.UIMenuCheckboxItem;
const UIMenuSliderItem = NativeUI.UIMenuSliderItem;
const BadgeStyle = NativeUI.BadgeStyle;
const Point = NativeUI.Point;
const ItemsCollection = NativeUI.ItemsCollection;
const Color = NativeUI.Color;
const ListItem = NativeUI.ListItem;

mp.gui.cursor.visible = false;
mp.gui.chat.show(false);

const arbeitsamt = new Menu("Arbeitsamt", "Jobs annehmen oder kündigen", new Point(100, 100));
arbeitsamt.AddItem(new UIMenuItem("Berufskraftfahrer", "Liefere Holz oder Kraftstoff in und außerhalb von Los Santos."));
arbeitsamt.AddItem(new UIMenuItem("Busfahrer", "Fahre mit einem Bus durch Los Santos"));

arbeitsamt.AddItem(new UIMenuItem("Kündigen", "Kündige deinen aktuellen Beruf."));

const closeItem = new UIMenuItem("Schließen", "Dieser Button schließt das Menü wieder.");
closeItem.BackColor = new Color(255, 0, 0, 255);
arbeitsamt.AddItem(closeItem);

arbeitsamt.Visible = false;

arbeitsamt.ItemSelect.on(item => {
	if (item instanceof UIMenuItem) 
	{
		if(item.Text == "Schließen")
		{
			mp.gui.chat.show(true);
			arbeitsamt.Close();
			Player.freezePosition(false);
		}
		else if(item.Text == "Kündigen")
		{
			mp.events.callRemote('Arbeitsamt_Kündigen');
			mp.gui.chat.show(true);
			arbeitsamt.Close();
			Player.freezePosition(false);
		}
		else if(item.Text == "Berufskraftfahrer")
		{
			mp.events.callRemote('Arbeitsamt_Berufskraftfahrer');
			mp.gui.chat.show(true);
			arbeitsamt.Close();
			Player.freezePosition(false);
		}
		else if(item.Text == "Busfahrer")
		{
			mp.events.callRemote('Arbeitsamt_Busfahrer');
			mp.gui.chat.show(true);
			arbeitsamt.Close();
			Player.freezePosition(false);
		}
	}
});

mp.events.add("Arbeitsamt", () => {
	arbeitsamt.Open();
	Player.freezePosition(true);
	mp.gui.chat.show(false);
});

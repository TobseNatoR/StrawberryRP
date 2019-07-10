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

const stadthalle = new Menu("Stadthalle", "Stadthalle Service", new Point(100, 100));
stadthalle.AddItem(new UIMenuItem("Personalausweis beantragen", "Die Kosten des Personalausweises belaufen sich auf 100$."));

const closeItem = new UIMenuItem("Schließen", "Dieser Button schließt das Menü wieder.");
closeItem.BackColor = new Color(255, 0, 0, 255);
stadthalle.AddItem(closeItem);

stadthalle.Visible = false;

stadthalle.ItemSelect.on(item => {
	if (item instanceof UIMenuItem) 
	{
		if(item.Text == "Schließen")
		{
			mp.gui.chat.show(true);
			stadthalle.Close();
			Player.freezePosition(false);
		}
		else if(item.Text == "Personalausweis beantragen")
		{
			mp.events.callRemote('Personalausweis');
			mp.gui.chat.show(true);
			stadthalle.Close();
			Player.freezePosition(false);
		}
	}
});

mp.events.add("Stadthalle", () => {
	stadthalle.Open();
	mp.gui.chat.show(false);
	Player.freezePosition(true);
});

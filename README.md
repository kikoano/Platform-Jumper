# Platform-Jumper
Проектна задача во C# Windows Forms.
## 1.Oпис на Апликацијата
Апликацијата е 16bit платформа игра(2D). Играта е направена со помош на Windows Forms co користење на Timer, Paint и Controls.
## 2. Упатство за користење
### 2.1 Мени
![](https://github.com/kikoano/Platform-Jumper/blob/master/screenshots/menu.png)
Co кликнување на Play се почнува со играње на играта.
![](https://github.com/kikoano/Platform-Jumper/blob/master/screenshots/highScores.png)
HighScores ги прикажува играчите со нивните освоени поени и време на поминување на нивоата подредени по
освоени поени почнувајки од најголемиот. How to Play e кратко објаснување на контролите на играчот и како да се
игра.
![](https://github.com/kikoano/Platform-Jumper/blob/master/screenshots/options.png)
Во Options можете да исклучите/вклучите звуците од играта. Exit ja исклучува апликацијата.
### 2.2 Контроли за играње
A - за одење на лево<br>
D - за одење на десно<br>
SPACE - за скок<br>
### 2.3 Целта на играчот
![](https://github.com/kikoano/Platform-Jumper/blob/master/screenshots/player.png)<br>
Целта на играчот е да ги победи двата нивоа во играта со највеке што може да земе парички(поени) и со најкратко време на поминување на нивоата.
<br>![](https://github.com/kikoano/Platform-Jumper/blob/master/screenshots/door.png)<br>
Врата ве носи во следното ниво.
<br>![](https://github.com/kikoano/Platform-Jumper/blob/master/screenshots/coins.png)<br>
Паричките ги има насекаде низ нивоата кои немора да ги соберете сите ако не сте сигурни дека можете да ги
земете.
<br>![](https://github.com/kikoano/Platform-Jumper/blob/master/screenshots/goblin.png)   ![](https://github.com/kikoano/Platform-Jumper/blob/master/screenshots/firehead.png)   ![](https://github.com/kikoano/Platform-Jumper/blob/master/screenshots/pit.png)<br>
Играчот има 5 животи. Животот се губи ако удрите во Goblin, Firehead или да паднете во дупка. Откога ќе изгубите
живот се ресетира на нивото што сте го изгубиле животот и ги губите паричките што сте ги собрале во тоа ниво.
Исто така времето за тоа ниво се ресетира.
<br>![](https://github.com/kikoano/Platform-Jumper/blob/master/screenshots/charge.png)   ![](https://github.com/kikoano/Platform-Jumper/blob/master/screenshots/fallOnHead.png)<br>
Можете да ги убиете гоблините така што кога сте во скок да ги допрете или додека паѓате да паднете на нивната
глава. Кога ќе ги убиете добивате автоматцки скок. Докулку ги изгубите сите животи или победите двата нивоа се
отвара прозорец кој ви кажива колку вкупно поени сте собрале и за колку време сте го победиле нивото. Исто така
внесувате име кое ќе биде зачувано во HighScores заедно со поените и времето. Вкупно поени и време се зима само
на победени нивоа, тоа значи ако ги изгубите сите пет животи на првото ниво ќе имате 0 поени и нема да имате
време(односно 0 време).
![](https://github.com/kikoano/Platform-Jumper/blob/master/screenshots/complete.png)
## 3. Работа на Timer и GameStateManager
### 3.1 Timer
Timer e иницијализиран на почетокот со ```timer.Interval = 1000 / 60;```Со ова кажуваме дека во секоја секунда
измината од Timer ќе направи 60 Ticks. Bo tick методот правиме
```
        private void timerTick(object sender, EventArgs e)
        {
            currentTime = DateTime.Now;
            gsm.Update((float)(currentTime - lastTime).TotalSeconds);
            gsm.Render();
            lastTime = currentTime;
        }
```
Каде ги повикуваме Update и Render методите од GameStateManager. Исто така многу важно е да се пресмета
delta(делта) време. Delta време се пресметува тако што од сегашното време се одзима времето од претходниот пат
кога било направено Tick. Ова делта време е многу важно затоа што Timer неправи точен Tick на даден интервал
секогаш има некој мал delay во милисекунди кој секогаш е разпичен(кај послап CPU ќе има поголем delay). За да го
надокнадиме овој delay, све што е во Update што треба да се движи(зголемување/намалување на Х и Y) ce множи по
delta.
### 3.2 GameStateManager
GameStateManager e менаџер на GameState класи кои се сместени во Stack, Тој менува, додава или брише GameState
класи од Stack-от. Тој прави Update и Render на GameState кој е на врвот на Stack-от. Секој GameState има референца
од GameStateManager. Ова овозможува лесен начин на промена на мени или ниво во играта.
## 4. Брз начин на манипулација со пиксели(pixels), читање и запишување пиксели во Bitmap
### 4.1 Sprites
При статрување на апликацијата се иницијализираат статичните Sprites објекти кои од Bitmap ги зиммат сите бои на
пикселите(alpha, red, green. blue) и ги сместуваат во byte низа. Со ова се овозможува само еднаш читање на Bitmap и
многу го намалува користењето на процесорот. Со зачување на сите Sprites во нивни byte низи зафаќа само ~27MB
process memory.
### 4.2 Screen
Screen класата има byte низа од пиксели од целата Bitmap на формата. Screen класата има методи кои ги зимаат
пикселите од Sprite и ги става во byte низата од Screen на точно кажани Х и Y кординати. Ако некој пиксел има боја
```Red == 255 && Green == 0 && Blue == 255```(розева боја) тој нема да биде ставен во byte низата од Screen.<br>
Пример
![](https://github.com/kikoano/Platform-Jumper/blob/master/Platform%20Jumper/Textures/goblin.png)<br>
Screen методите се повукуваат секогаш во Render методот на GameState од кој е па повикан од GameStateManager.
Кога GameStateManager ќе изврши Render тој повукува```Invalidate();```од формата. Invalidate повикува
```
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            screen.PixelsToBitmap();
            e.Graphics.DrawImage(screen.ScreenMap, 0, 0, Form1.WIDTH * Form1.SCALE, Form1.HEIGHT * Form1.SCALE);
        }
```
Каде ```PixelsToBitmap()```методот ги зима byte array пикселите од Screen и ги сместува во Bitmap ```ScreenMap```.
Потоа се црта Bitmap ```ScreenMap```на формата зголемена за 3 пати затоа што сликата е многу мала затоа што работиме со
16x16 слики. Со цртање Bitmap на екран само откога ќе се извршат сите манипулации со пиксели повикувани од
GameState и Entity класи(све ова се случува во еден tick) ce користи многу малку од процесорот што е многу добро.
![](https://github.com/kikoano/Platform-Jumper/blob/master/screenshots/process.png)<br>
## 5. Читање на нивото во играта
![](https://github.com/kikoano/Platform-Jumper/blob/master/Platform%20Jumper/Levels/level1.png)   ![](https://github.com/kikoano/Platform-Jumper/blob/master/Platform%20Jumper/Levels/level2.png)<br>
Нивоата се слика со одредени бои на пиксели на одредени позиции. LevelState(кoj e екстензија од GameState) при
иницијализација го повикува методот```LoadMapToTiles(string path)```кој ја наоѓа сликата преку дадената патека и
ги чита сите бои на пикселите. со помош на структурата(struct) Colors имаме методи за препознавање на боите на
пикселот за што значат. На пример<br>
```
            public bool isWall()
            {
                if (Red == 64 && Green == 64 && Blue == 64)
                    return true;
                return false;
            }
```
дава true ако боите на тој пиксел се тие вредности откога ќе најде што треба тој пиксел да биде го сместува во низа
од int(TiIes кој се наоѓа во LeveIState) или во Entities што е Dictionary(клучот се пресметува```(x * 4) + (y * 16 * Width)```
каде x и у se позицијата од ```BitmapData mapData```)некои бои на пиксели може да кажуваат и tile и entity. Tiles
ce сите статички работи како на пример sид, позадина sид, трева. земја, кутија...и др. Tiles имаат само renderTiIesO
метод каде за одреден број сто бил даден од LoadMapToTiles медотод ќе репрезентира некој Sprite. Пример
```
                    if (Tiles[x + y * Width] == 6)
                        gsm.screen.RenderSprite(x * 16, y * 16, Sprite.Wall1, false);
```
Додека Entities e све што е динамичко и иммат Render и Update методи. Во Entities спаѓаат играчот, гоблии, огнена
глава и парички.
## 6. Entities update
Да кажеме имаме 200 Entities на нивото. Дали за сите Entities ќе правиме update? Замислете кога играчот треба да
најде колизија со некој entity, тој ќе треба да ги провери сите Entities за да види со кој има колизија. Или па движење
на entity кој играчот не го гледа на екран. Затоа со методот ```findScreenEntities()```во LevelState ќе ги најдеме
Entities и ги сместува во ScreenEntities оние само кои што се околу играчот со должина и ширина на наоѓање за 2
или 3 пати повеке од тоа што е на екранот. Кога играчот ќе помине пола од тоа растојание повторно ќе наоѓа
enteties. Ова прави илузија на играчот така да секогаш он ќе ги гледа тие Entities кој се активни(иммат Update).
<br>Пример:<br>
![](https://github.com/kikoano/Platform-Jumper/blob/master/screenshots/scan1.png)<br>
Сите enteties кои се само внатре во црвената линија ќе ги смести во ScreenEntities и само за тие ќе врши Update.
![](https://github.com/kikoano/Platform-Jumper/blob/master/screenshots/scan2.png)<br>
Потоа кога ќе измине пола пат од тоа скенирано растојание повторно ќе изврши```findScreenEntities()```. Сите
enteties кои се само внатре во плавата линија ќе ги смести во ScreenEntities(тие Entities порано што ги имало а сега
ги нема, нема да ги има во листата) и само за тие ќе врши Update.
## 7. Windows forms controls
Секој GameState има листа од controls каде имаат дефинирано виртуелни методи за иницијализација и бришење на
контролите. Ова овозможува лесен начин на иницијализирање на контролите кога ќе се додаде нов GameState во
Stack или па кога ќе се избрише GameState од Stack.
```
        public virtual void Init()
        {
            controls = new List<Control>();
        }
        public virtual void Cleanup()
        {
            foreach (Control c in controls)
            {
                gsm.Form.Controls.Remove(c);
                c.Dispose();
            }
        }
```
Имаме дефинирано сопствени класи кои се екстензија од Button, Label, ListBox. Ha пример ButtonMenu e екстензија
од Button и имаме направено override на OnClick да пушти звук.
```
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Sound.Click.Play();
        }
```
Класата Sound има статички дефинирано објекти со нивна патека до звукот кои се пуштаат со помош на
SoundPlayer.
## 8. Снимање на HighScore
Имаме класа DataScore која е Serializable за да може да се зачува. Во неја се чуваат името на играчот. вкупно поени
и вкупно време. Одкога играчот ќе биде прашан да внесе негово име(ова е во LevelCompleteState) кога ќе го
притисне копчето Continue се извршува методот ```CreateDataScore()```. Кој прави DataScore објект од вредностите од
корисникот и ја залепува класата(Append)```\Data\scores.bin```(ако непостои ќе направи нова). Потоа во
HighScoreState со методот```loadDataScore()```ги вчитува DataScore објектите од ```\Data\scores.bin``` , ги сместува во
листа и ги сортира по големина на освоени поени. Има 3 CustomListBox(кои се екстензија од ListBox) во кои ги става
името, поените и времето соодветно.
![](https://github.com/kikoano/Platform-Jumper/blob/master/screenshots/highScores.png)














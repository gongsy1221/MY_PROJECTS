#include<iostream>
#include<fcntl.h>
#include<io.h>
#include<Windows.h>
#include "TitleScene.h"
#include "console.h"
#include "mci.h"
using namespace std;



MENU Title()
{
	while (true)
	{
		TitleRender();
		switch (MenuRender())
		{
		case MENU::START:
			EnterAnimation();
			return MENU::START;
		case MENU::INFO:
			InfoRender();
			return MENU::INFO;
		case MENU::QUIT:
			return MENU::QUIT;
		}
	}
}

void TitleRender()
{
	srand((unsigned int)time(NULL));
	int prevmode = _setmode(_fileno(stdout), _O_U16TEXT);

	ChangeTitleColor();

	int curmode = _setmode(_fileno(stdout), prevmode);
	PlayBgm(TEXT("BGM.mp3"), 100);
}

MENU MenuRender()
{
	COORD Resolution = GetConsoleResolution();
	int x = Resolution.X / 2.5;
	int y = Resolution.Y / 2;
	int originy = y;
	Gotoxy(x, y);
	cout << "게임 시작";
	Gotoxy(x, y + 1);
	cout << "게임 정보";
	Gotoxy(x, y + 2);
	cout << "종료 하기";
	while (true)
	{
		KEY eKey = KeyController();
		switch (eKey)
		{
		case KEY::UP:
		{
			if (y > originy)
			{
				Gotoxy(x - 2, y);
				cout << " ";
				Gotoxy(x - 2, --y);
				SetColor((int)COLOR::LIGHT_YELLOW);
				cout << ">";
				SetColor((int)COLOR::WHITE);
				ChangeTitleColor();
				Sleep(100);
			}
		}
		break;
		case KEY::DOWN:
		{
			if (y < originy + 2)
			{
				Gotoxy(x - 2, y);
				cout << " ";
				Gotoxy(x - 2, ++y);
				SetColor((int)COLOR::LIGHT_YELLOW);
				cout << ">";
				SetColor((int)COLOR::WHITE);
				ChangeTitleColor();
				Sleep(100);
			}
		}
		break;
		case KEY::SPACE:
			//PlayEffect(TEXT("ButtonSelect.wav"));
			PlaySound(TEXT("ButtonSelect.wav"), 0, SND_FILENAME | SND_ASYNC);
			if (originy == y)
			{
				FlickerAnimation(x, y);
				return MENU::START;
			}
			else if (originy + 1 == y)
			{
				FlickerAnimation(x, y);
				return MENU::INFO;
			}
			else if (originy + 2 == y)
			{
				FlickerAnimation(x, y);
				return MENU::QUIT;
			}
		}
	}
}

void InfoRender()
{
	system("cls");

	SetColor((int)COLOR::SKYBLUE);
	cout << "[조작법]" << endl;
	SetColor((int)COLOR::WHITE);
	cout << "도망자(☆) : WASD" << endl;
	cout << "술래(★)  : 방향키" << endl;

	cout << endl;

	SetColor((int)COLOR::SKYBLUE);
	cout << "[게임 플레이 방법]" << endl;
	SetColor((int)COLOR::WHITE);
	cout << "도망자는 토큰을 모두 먹을 시 승리," << endl;
	cout << "술래는 도망자를 잡으면 승리합니다." << endl;
	
	cout << endl;

	cout << "토큰(♭)  : 도망자가 먹어야하는 토큰입니다." << endl;
	cout << "아이템(□) : 아이템은 도망자, 술래 둘 다 획득 가능하며," << endl;
	cout << "             아이템 획득 시 술래와 도망자 역할이 바뀝니다." << endl;

	cout << endl;
	SetColor((int)COLOR::LIGHT_RED);
	cout << "나가기 : 아래 방향키" << endl;
	SetColor((int)COLOR::WHITE);

	while (true)
	{
		if (KeyController() == KEY::DOWN)
		{
			system("cls");
			break;
		}
	}
}

KEY KeyController()
{
	if (GetAsyncKeyState(VK_UP) & 0x8000)
	{
		//Sleep(100);
		return KEY::UP;
	}
	if (GetAsyncKeyState(VK_DOWN) & 0x8000)
	{
		return KEY::DOWN;
	}
	if (GetAsyncKeyState(VK_SPACE) & 0x8000)
	{
		return KEY::SPACE;
	}
	//Sleep(100);
	return KEY::FAIL;
}

void EnterAnimation()
{
	COORD Resolution = GetConsoleResolution();
	int width = Resolution.X;
	int height = Resolution.Y;
	int animtime = 20;

	SetColor((int)COLOR::BLACK, (int)COLOR::LIGHT_GRAY);
	for (int i = 0; i < height; ++i)
	{
		for (int j = 0; j < width  /2; j++)
		{
			Gotoxy(j * 2, i);
			cout << "  ";
		}
		Sleep(animtime);
	}
	SetColor((int)COLOR::SKYBLUE, (int)COLOR::BLACK);
	for (int i = 0; i < height; ++i)
	{
		for (int j = 0; j < width / 2; j++)
		{
			Gotoxy(j * 2, i);
			cout << "  ";
		}
		Sleep(animtime);
	}
	SetColor((int)COLOR::WHITE, (int)COLOR::BLACK);
	system("cls");
}

void FlickerAnimation(int x,int y)
{
	COORD Resolution = GetConsoleResolution();
	int originY = Resolution.Y / 2;
	string selectBtn;

	if (y == originY)
		selectBtn = "게임 시작";
	else if (y == originY + 1)
		selectBtn = "게임 정보";
	else if (y == originY + 2)
		selectBtn = "종료 하기";

	SetColor((int)COLOR::LIGHT_YELLOW);
	for (int i = 0; i < 5; i++)
	{
		Gotoxy(x, y);
		if(i % 2 ==0)
			cout << selectBtn;
		else
			cout << "          ";
		Sleep(300);
	}
	SetColor((int)COLOR::WHITE);
}

void ChangeTitleColor()
{
	Gotoxy(0, 0);

	int randnum = rand() % 15 +1;
	SetColor(randnum);

	wcout << L"                                                                                                        " << endl;
	wcout << L"      _  .-')                   .-') _ ,---.                   ('-.     .-') _             ('-. .-.,---." << endl;
	wcout << L"     ( \\( -O )                 ( OO ) )|   |                  ( OO ).-.(  OO) )           ( OO )  /|   |" << endl;
	wcout << L"      ,------. ,--. ,--.   ,--./ ,--,' |   |         .-----.  / . --. //     '._  .-----. ,--. ,--.|   |" << endl;
	wcout << L"      |   /`. '|  | |  |   |   \\ |  |\\ |   |        '  .--./  | \\-.  \\ |'--...__)'  .--./ |  | |  ||   |" << endl;
	wcout << L"      |  /  | ||  | | .-') |    \\|  | )|   |        |  |('- ) '-'  |  |'--.  .--'|  |('-. |   .|  ||   |" << endl;
	wcout << L"      |  |_.' ||  |_|( OO )|  .     |/ |  .'       /_) |OO  ) | |_.'  |   |  |  /_) |OO  )|       ||  .'" << endl;
	wcout << L"      |  .  '.'|  | | `-' /|  |\\    |  `--'        ||  |`-'|  |  .-.  |   |  |  ||  |`-'| |  .-.  |`--' " << endl;
	wcout << L"      |  |\\  \\('  '-'(_.-' |  | \\   |  .--.       (_'  '--'\\  |  | |  |   |  | (_'  '--'\\ |  | |  |.--. " << endl;
	wcout << L"      `--' '--' `-----'    `--'  `--'  '--'          `-----'  `--' `--'   `--'    `-----' `--' `--''--' " << endl;

	SetColor((int)COLOR::WHITE);
}
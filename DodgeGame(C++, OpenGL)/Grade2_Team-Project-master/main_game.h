#pragma once
#include "game.h"
// (4) GAME FUNCTION
#define WIN_WIDTH 800
#define WIN_HEIGHT 600

#define POOL_SIZE					600

#define GAME_UPDATE_INTERVAL		2
#define READY_INTERVAL				50
#define ENEMY_SPAWN_INTERVAL		50
#define RANDOM_SPAWN_INTERVAL		20
#define STRONG_ENEMY_SPAWN_SEC		20

#define BULLET_PROBABILITY			0.025f
#define ITEM_HEALTH_PROBABILITY		0.001f
#define ITEM_POWER_PROBABILITY		0.001f
#define ITEM_SHIELD_PROBABILITY		0.0005f

#define PLAYER_SPEED				5.0f
#define ENEMY_X_SPEED				2.0f
#define ENEMY_Y_SPEED				1.0f
#define BULLET_SPEED				6.0f
#define ITEM_HEALTH_SPEED			2.0f
#define ITEM_POWER_SPEED			2.0f

#define INITIAL_LIFE				3
#define INITIAL_ENERGY				100
#define HIT_DAMAGE					10	//���� ���
#define HIT_SCORE					5	//�ı� ȹ�� ����
#define HIT_DELAY					60	//�ǰ� ������
#define ITEM_HEALTH_ENERGY			20	//������ ȸ�� ü��
#define INITIAL_BOSS_ENERGY			1500	//���� ü��



GameObject* player_obj = NULL;
GameObject* player_back_obj = NULL;
GameObject* boss_obj = NULL;
Bitmap* background = NULL;
Bitmap* title_image = NULL;
Bitmap* explain_image = NULL;
Bitmap* button_start = NULL;
Bitmap* button_explain = NULL;
Bitmap* button_exit = NULL;
Bitmap* ending_image = NULL;
Bitmap* clear_image = NULL;
Bitmap* boss_detected_image = NULL;
Bitmap* shield_image = NULL;
Bitmap* bomb_image = NULL;
Bitmap* life_image1 = NULL;
Bitmap* life_image2 = NULL;
Bitmap* life_image3 = NULL;

void createPlayer();
void createEnemy_1();
void createEnemy_2();
void createEnemy_3();
void createEnemy_4();
void createBoss();

void createPlayerBullet(int bullet_direction);
void createPlayerBomb();
void createEnemyBullet(GameObject* enemy);
void createBossBullet(GameObject* boss);

void createItemHealth();
void createItemPower();
void createItemShield();

void createParticles(GameObject* obj);
void createDieParticles2();
void createBombparticle();
void createShieldParticles(GameObject* obj);
void createMeteor();
void createGuidedBullet();
void createShields(GameObject* obj);

void moveEnemy(GameObject* enemy);
void moveBoss(GameObject* boss);

void movePlayerBullet(GameObject* bullet);
void moveEnemyBullet(GameObject* bullet);
void moveMeteor(GameObject* bullet);		//�ʱ� �������� �
void moveGuidedBullet(GameObject* bullet);	//���� �

void moveItemHealth(GameObject* item);
void moveItemPower(GameObject* item);
void moveItemShield(GameObject* item);
void moveParticle(GameObject* particle);
void moveBomb(GameObject* bomb);
void movePlayer_BeforeBoss(int time);

void checkCollisions();
void select_direction(GameObject* enemy);
void diemotion_createItemPower();

// Player state
int delay = HIT_DELAY;	//�ǰ� ������
int num_life = 0;
int score = 0;
int power = 1;
int play_age = 0;
float play_sec = 0;
int player_bullet_count = 0;	//�Ѿ� ���� �������� ����

int ready_count = 0;
int menu_selection = 0;
int shield = 0;			//shield
int prevbomb_count = 0;	//���� ��ź ����
int bomb = 0;			//bomb

// Boss pattern
enum {
	PATTERN1,
	PATTERN2,
	PATTERN3,
	PATTERN4,
};
int pattern_end = 0;	//���� ���� ���� �ľ�
int pattern_time = 600;	//���� ���� ���� �ð�
int ready_bullet = 0;	//���� ���� �غ� �Ϸ� �ľ�
//���� �Ѿ� �߻� ���� �ּ� ��ȯ �Ÿ�
float targetX = 0, targetY = 0;
//dwidth = 53
float dwidth = (float)WIN_WIDTH / (pattern_time / 40.0f);
float dheight = (float)WIN_HEIGHT / (pattern_time / 40.0f);
int boss_pattern = PATTERN3;
void changeBossPattern(int pattern);

//Draw
void drawMenu();
void drawPlayerStates();
void drawBossStates();
void drawBossDetected();
void drawGameOver();
void drawGameClear();
void drawExplain();
void draw_score_Timer();

void handleSpecialKey2();
void handleGeneralKey2();

// Game state
enum
{
	OUT_OF_GAME = -1,	//���� ������
	PRE_GAME,			//���� ���� ����
	EXPLAIN_GAME,		//How To Use ȭ��
	READY_GAME,			//���� �غ�(�׾��� �� ���� �ð�)
	IN_GAME,			//���� ���� ��
	POST_GAME,			//���� ����
	READY_BOSS,			//������ �غ�
	IN_BOSS,			//���� ������
	CLEAR_GAME,			//���� Ŭ����
};

int game_mode = OUT_OF_GAME;
void changeGameMode(int new_mode);
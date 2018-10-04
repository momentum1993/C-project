#include "main_game.h"

//연제원//신태욱//양지용
//2016.12.15
//고급 C프로그래밍 슈팅게임 제작 프로젝트

void main()
{
	initVisualApp( WIN_WIDTH, WIN_HEIGHT );
}

//시작전 실행 함수
void initialize()
{
	initializePool( POOL_SIZE );
	changeGameMode(PRE_GAME);
}

//끝내기전 실행 함수
void finalize()
{
	changeGameMode(OUT_OF_GAME);
	finalizePool();
}

//draw 함수
void draw()
{
	static int count = 0;

	if (background)
	{
		drawBitmap(background, WIN_WIDTH / 2, WIN_HEIGHT / 2, WIN_WIDTH, WIN_HEIGHT, 0);
	}

	int i;
	for (i = 0; i < POOL_SIZE; i++)
	{
		GameObject* game_obj = getObject(i);
		if (game_obj) {
			if (game_obj->is_alive && game_obj->bitmap)
			{
				if (ready_count > 0 && ((game_obj == player_obj) || (game_obj == player_back_obj)) && ready_count % 5 == 0)
				{
					continue;
				}
				drawBitmap(game_obj->bitmap, game_obj->x, game_obj->y, game_obj->width, game_obj->height, game_obj->a);
			}
		}
	}

	switch (game_mode)
	{
	case PRE_GAME:	
		drawMenu();
		break;
	case EXPLAIN_GAME:
		drawExplain();
		break;

	case READY_GAME:
	case IN_GAME:
		drawPlayerStates();
		draw_score_Timer();
		break;
	case READY_BOSS:
		drawPlayerStates();
		draw_score_Timer();
		drawBossDetected();
		drawBossStates();
		break;
	case IN_BOSS:
		drawPlayerStates();
		draw_score_Timer();
		drawBossStates();
		break;

	case POST_GAME:
		drawGameOver();
		break;
	case CLEAR_GAME:
		drawGameClear();
		break;
	}

	count++;
}

//update 함수
void update()
{
	static int count = 1;
	static int num_frames = 1;//생성 주기를 제공하기 위한 변수

	if (count % GAME_UPDATE_INTERVAL == 0)
	{
		int i;
		for (i = 0; i < POOL_SIZE; i++)
		{
			GameObject* game_obj = getObject(i);
			if (game_obj->is_alive)
			{
				switch (game_obj->type) {
				case ENEMY1:
				case ENEMY2:
				case ENEMY3:
				case ENEMY4:
					if(game_obj->age <= 10) game_obj->age++;
					moveEnemy(game_obj);
					if (game_obj->age > 10)
					{
						if (randomFloat(0, 1) < BULLET_PROBABILITY)
						{
							createEnemyBullet(game_obj);
						}
					}
					break;

				case PLAYER_BULLET:
					movePlayerBullet(game_obj);
					break;

				case ENEMY_BULLET:
					moveEnemyBullet(game_obj);
					break;

				case PARTICLE:
				case BOMB_PARTICLE:
					moveParticle(game_obj);
					game_obj->age++;
					break;

				case ITEM_HEALTH:
					moveItemHealth(game_obj);
					game_obj->age++;
					break;

				case ITEM_POWER:
					moveItemPower(game_obj);
					game_obj->age++;
					break;
				case ITEM_SHIELD:
					moveItemShield(game_obj);
					game_obj->age++;
					break;

				case PLAYER_BOMB: 
					moveBomb(game_obj);
					break;

				case METEOR:
					moveMeteor(game_obj);
					break;
				case GUIDED_BULLET:
					moveGuidedBullet(game_obj);
					break;

				case BOSS:
					if (pattern_end == 1) {
						changeBossPattern(boss_pattern);
						pattern_end = 0;
					}
					else {
						moveBoss(boss_obj);
					}
					break;
				}
				
			}
		}

		if (game_mode == IN_GAME)
		{
			//add playTime
			if (play_age++ >= 1) {
				play_sec += (float)1/20;
				play_age = 0;
			}
			//create Bomb
			if (score / 2000 > prevbomb_count) {
				prevbomb_count++;
				bomb++;
			}

			//LEVEL 1
			if (play_sec <= STRONG_ENEMY_SPAWN_SEC) {
				if (num_frames % ENEMY_SPAWN_INTERVAL == 0) {
					createEnemy_1();
				}
				if (num_frames % RANDOM_SPAWN_INTERVAL * 2.0 == 0) {
					createMeteor();
				}
			}
			//LEVEL 2
			else if ((play_sec > STRONG_ENEMY_SPAWN_SEC) && (play_sec < STRONG_ENEMY_SPAWN_SEC * 2)) {
				if (num_frames % ENEMY_SPAWN_INTERVAL / 6.0 == 0) {
					createEnemy_2();
				}
				if (num_frames % RANDOM_SPAWN_INTERVAL * 1.5 == 0) {
					createMeteor();
				}
			}
			//LEVEL 3
			else if ((play_sec >= STRONG_ENEMY_SPAWN_SEC * 2) && (play_sec < STRONG_ENEMY_SPAWN_SEC * 3)) {
				if (num_frames % ENEMY_SPAWN_INTERVAL / 12.0 == 0) {
					createEnemy_3();
				}
				if (num_frames % RANDOM_SPAWN_INTERVAL == 0) {
					createMeteor();
				}
			}
			//LEVEL 4
			else if ((play_sec >= STRONG_ENEMY_SPAWN_SEC * 3) && (play_sec < STRONG_ENEMY_SPAWN_SEC * 4)) {
				if (num_frames % ENEMY_SPAWN_INTERVAL / 12.0 == 0) {
					createEnemy_4();
				}
				if (num_frames % RANDOM_SPAWN_INTERVAL / 1.5 == 0) {
					createMeteor();
				}
			}
			//LEVEL 5
			else if (play_sec >= STRONG_ENEMY_SPAWN_SEC * 4) {
				if (num_frames % ENEMY_SPAWN_INTERVAL / 20.0 == 0) {
					createEnemy_4();
				}
				if (num_frames % RANDOM_SPAWN_INTERVAL / 3.0 == 0) {
					createMeteor();
				}
				if (num_frames % RANDOM_SPAWN_INTERVAL / 5.0 == 0) {
					createGuidedBullet();
				}
			}
			//LEVEL 6
			else if ((play_sec >= STRONG_ENEMY_SPAWN_SEC * 4) && (play_sec < STRONG_ENEMY_SPAWN_SEC * 5)) {
				if (num_frames % ENEMY_SPAWN_INTERVAL / 20.0 == 0) {
					createEnemy_4();
				}
				if (num_frames % RANDOM_SPAWN_INTERVAL / 3.0 == 0) {
					createMeteor();
				}
				if (num_frames % RANDOM_SPAWN_INTERVAL / 3.0 == 0) {
					createGuidedBullet();
				}
			}

			if ((play_sec >= STRONG_ENEMY_SPAWN_SEC * 5)) {
				changeGameMode(READY_BOSS);
			}

			//createItemHealth
			if (randomFloat(0, 0.5) < ITEM_HEALTH_PROBABILITY)
			{
				createItemHealth();
			}
			//createItemPower
			if (randomFloat(0, 0.5) < ITEM_POWER_PROBABILITY)
			{
				createItemPower();
			}
			//createItemShield
			if (randomFloat(0, 0.5) < ITEM_SHIELD_PROBABILITY)
			{
				createItemShield();
			}
			checkCollisions();
		}

		if (game_mode == READY_GAME)
		{
			ready_count--;

			if (ready_count == 0)
			{
				changeGameMode(IN_GAME);
			}
		}
		if (game_mode == READY_BOSS) {
			ready_count--;
			movePlayer_BeforeBoss(ready_count);
			if (ready_count == 0) {
				changeGameMode(IN_BOSS);
			}
		}

		if (game_mode == IN_BOSS) {
			if (boss_obj) {
				if (boss_obj->health <= 0) {
					changeGameMode(CLEAR_GAME);
				}
			}
			if (play_age++ >= 1) {
				play_sec += (float)1 / 20;
				play_age = 0;
			}
			//create Bomb
			if (score / 2000 > prevbomb_count) {
				prevbomb_count++;
				bomb++;
			}
			//create Boss Bullet
			if (ready_bullet) {
				createBossBullet(boss_obj);
			}
			//createItemHealth
			if (randomFloat(0, 1) < ITEM_HEALTH_PROBABILITY)
			{
				createItemHealth();
			}
			//createItemPower
			if (randomFloat(0, 1) < ITEM_POWER_PROBABILITY)
			{
				createItemPower();
			}
			//createItemShield
			if (randomFloat(0, 1) < ITEM_SHIELD_PROBABILITY)
			{
				createItemShield();
			}
			checkCollisions();
		}

		if (game_mode == POST_GAME) {
			//create Boss Bullet
			if (ready_bullet) {
				createBossBullet(boss_obj);
			}
		}
		//1프레임 증가
		num_frames ++;
		if (num_frames == 60) num_frames = 0;
	}
	handleSpecialKey2();
//	handleGeneralKey2();
	count++;
	if (count == 100) count = 0;
}

//메뉴에서의 방향키
void handleSpecialKey( int key, int x, int y )
{
	switch (key)
	{
		case GLUT_KEY_UP:
			if (game_mode == PRE_GAME)
			{
				menu_selection--;
				if (menu_selection < 0) menu_selection = 2;
			}
			break;

		case GLUT_KEY_DOWN:
			if (game_mode == PRE_GAME)
			{
				menu_selection++;
				if (menu_selection > 2) menu_selection = 0;
			}
			break;
	}
}

//메뉴 이외에서의 공격키, 엔터
void handleGeneralKey(unsigned char key, int x, int y)
{
	switch (key)
	{
	case 13:		// enter
	{
		if (game_mode == PRE_GAME)
		{
			if (menu_selection == 0)
			{
				changeGameMode(READY_GAME);
			}
			else if (menu_selection == 1)
			{
				changeGameMode(EXPLAIN_GAME);
			}
			else if (menu_selection == 2)
			{
				finalize();
				exit(0);
			}
		}
		else if ((game_mode == POST_GAME) || (game_mode == CLEAR_GAME) || (game_mode == EXPLAIN_GAME))
		{
			changeGameMode(PRE_GAME);
		}
	}
	break;

	case 68:	// D //EAST
	case 100:	// d
	{
		if (game_mode == IN_GAME) {
			createPlayerBullet(EAST);
		}
	}
	break;
	case 65:	// A //WEST
	case 97:	// a
	{
		if (game_mode == IN_GAME) {
			createPlayerBullet(WEST);
		}
	}
	break;
	case 83:	// S //NORTH
	case 115:	// s
	{
		if (game_mode == IN_GAME) {
			createPlayerBullet(NORTH);
		}
	}
	break;
	case 87:	// W //SOUTH
	case 119:	// w
	{
		if ((game_mode == IN_GAME) || (game_mode == IN_BOSS)) {
			createPlayerBullet(SOUTH);
		}
	}
	break;
	case 90:   //Z
	case 122:   //z
	{
		if ((game_mode == IN_GAME) || (game_mode == IN_BOSS)) {
			if (bomb > 0) {
				createPlayerBomb();
				bomb--;
			}
		}
	}
	break;
	}
}

//인게임에서의 방향키
void handleSpecialKey2( )
{
	if ((game_mode == IN_GAME) || (game_mode == IN_BOSS)) {
		if ((GetAsyncKeyState(VK_UP) & 0x8000) && !(player_obj->y + player_obj->height / 2 >= WIN_HEIGHT)) {
			player_obj->y += PLAYER_SPEED;
			player_back_obj->y += PLAYER_SPEED;
		}
		if ((GetAsyncKeyState(VK_LEFT) & 0x8000) && !(player_obj->x - player_obj->width / 2 <= 0)) {
			player_obj->x -= PLAYER_SPEED;
			player_back_obj->x -= PLAYER_SPEED;
		}
		if ((GetAsyncKeyState(VK_RIGHT) & 0x8000) && !(player_obj->x + player_obj->width / 2 >= WIN_WIDTH) ) {
			player_obj->x += PLAYER_SPEED;
			player_back_obj->x += PLAYER_SPEED;
		}
		if ((GetAsyncKeyState(VK_DOWN) & 0x8000) && !(player_obj->y - player_obj->height / 2 <= 0) ) {
			player_obj->y -= PLAYER_SPEED;
			player_back_obj->y -= PLAYER_SPEED;
		}
	}
}

/*
void handleGeneralKey2()
{
	if ((GetAsyncKeyState(VK_RETURN) & 0x8000)) {
		if (game_mode == PRE_GAME) {
			if (menu_selection == 0) {
				changeGameMode(READY_GAME);
			}
			else {
				finalize();
				exit(0);
			}
		}
		else if (game_mode == POST_GAME) {
			changeGameMode(PRE_GAME);
		}
	}

	if(game_mode == IN_GAME){
		if ((GetAsyncKeyState(VK_SPACE) & 0x8000)) {
			createPlayerBullet();
		}
	}

}
*/

/*----------CREATE OBJECT FUNCTION----------*/

//플레이어 생성 함수
void createPlayer()
{
	if (player_obj) {
		return;
	}

	player_back_obj = newObject();
	player_obj = newObject();

	if ((player_obj) && (player_back_obj)) {
		player_back_obj->type = PLAYER_BACK;
		player_back_obj->bitmap = createBitmap("playerShip3_orange.png");
		player_back_obj->x = WIN_WIDTH / 2.0f;
		player_back_obj->y = 100.0f;
		player_back_obj->width = 80.0f;
		player_back_obj->height = 80.0f;
		player_back_obj->a = 0.0f;


		player_obj->type = PLAYER;
		player_obj->bitmap = createBitmap("ball.png");
		player_obj->x = WIN_WIDTH / 2.0f;
		player_obj->y = 100.0f;
		player_obj->vx = 0.f;
		player_obj->vy = 0.f;
		player_obj->health = INITIAL_ENERGY;
		player_obj->width = 10.0f;
		player_obj->height = 10.0f;
		player_obj->a = 0.0f;
	}
}

//1단계 적기 생성 함수
void createEnemy_1() {
	GameObject* enemy_obj = newObject();

	if (enemy_obj) {
		if (play_sec <= STRONG_ENEMY_SPAWN_SEC) {
			enemy_obj->type = ((float)rand() / (float)RAND_MAX < 0.7f ? ENEMY1 : ENEMY2);
			select_direction(enemy_obj);
			if (enemy_obj->type == ENEMY1) {
				enemy_obj->health = 1.0f;
			}
			else {
				enemy_obj->health = 2.0f;
			}
		}
	}
}

//2단계 적기 생성 함수
void createEnemy_2() {
	GameObject* enemy_obj = newObject();

	if (enemy_obj)
	{
		if ((play_sec > STRONG_ENEMY_SPAWN_SEC) && (play_sec < STRONG_ENEMY_SPAWN_SEC * 2)) {
			enemy_obj->type = ((float)rand() / (float)RAND_MAX < 0.7f ? ENEMY2 : ENEMY3);
			select_direction(enemy_obj);
			if (enemy_obj->type == ENEMY2) {
				enemy_obj->health = 3.0f;
			}
			else {
				enemy_obj->health = 5.0f;
			}
		}
	}
}

//3단계 적기 생성 함수
void createEnemy_3() {
	GameObject* enemy_obj = newObject();

	if (enemy_obj)
	{
		if (play_sec >= STRONG_ENEMY_SPAWN_SEC * 2 && (play_sec < STRONG_ENEMY_SPAWN_SEC * 3)) {
			float ranflot = randomFloat(0, 1);
			if (ranflot < 0.1f) enemy_obj->type = ENEMY2;
			else if (ranflot < 0.2f) enemy_obj->type = ENEMY3;
			else enemy_obj->type = ENEMY4;
			select_direction(enemy_obj);

			if (enemy_obj->type == ENEMY2) {
				enemy_obj->health = 3.0f;
			}
			else if (enemy_obj->type == ENEMY3) {
				enemy_obj->health = 5.0f;
			}
			else if (enemy_obj->type == ENEMY4) {
				enemy_obj->health = 9.0f;
			}
		}
	}
}

//4단계 적기 생성 함수
void createEnemy_4() {
	GameObject* enemy_obj = newObject();

	if (enemy_obj)
	{
		if (play_sec >= STRONG_ENEMY_SPAWN_SEC * 3) {
			float ranflot = randomFloat(0, 1);
			if (ranflot < 0.1f) enemy_obj->type = ENEMY2;
			else if (ranflot < 0.4f) enemy_obj->type = ENEMY3;
			else enemy_obj->type = ENEMY4;
			select_direction(enemy_obj);

			if (enemy_obj->type == ENEMY2) {
				enemy_obj->health = 4.0f;
			}
			else if (enemy_obj->type == ENEMY3) {
				enemy_obj->health = 5.0f;
			}
			else if (enemy_obj->type == ENEMY4) {
				enemy_obj->health = 8.0f;
			}
		}
	}
}

//보스 생성 함수
void createBoss() {

	if (boss_obj) {
		return;//	deleteObject(boss_obj);
	}

	boss_obj = newObject();

	if (boss_obj) {
		boss_obj->type = BOSS;
		boss_obj->bitmap = createBitmap("boss.png");
		boss_obj->x = WIN_WIDTH / 2.0f;
		boss_obj->y = WIN_HEIGHT - 150.0f;
		boss_obj->vx = 0.0f;
		boss_obj->vy = 0.0f;
		boss_obj->health = INITIAL_BOSS_ENERGY;
		boss_obj->width = 50.0f;
		boss_obj->height = 50.0f;
		boss_obj->a = 0.0f;
	}
}

/*----------CREATE FUNCTION----------*/

//플레이어 총알 생성함수
void createPlayerBullet(int bullet_direction)
{
	GameObject* bullet_obj = newObject();

	if ((bullet_obj) && (player_bullet_count <= 5)) {
		player_bullet_count += 1;
		bullet_obj->type = PLAYER_BULLET;
		bullet_obj->bitmap = createBitmap("laserBlue03.png");
		//EAST
		if (bullet_direction == EAST) {
			bullet_obj->direction = bullet_direction;
			bullet_obj->width = 20.0f * power * 0.5f;
			bullet_obj->height = 40.0f;
			bullet_obj->x = player_obj->x + player_back_obj->width / 2 + bullet_obj->width / 2;
			bullet_obj->y = player_obj->y;
			bullet_obj->vx = BULLET_SPEED * 2.0f;
			bullet_obj->vy = 0.0f;
			bullet_obj->a = 270.0f;
			player_back_obj->a = 270.0f;
		}//WEST
		else if (bullet_direction == WEST) {
			bullet_obj->direction = bullet_direction;
			bullet_obj->width = 20.0f * power * 0.5f;
			bullet_obj->height = 40.0f;
			bullet_obj->x = player_obj->x - player_back_obj->width / 2 - bullet_obj->width / 2;
			bullet_obj->y = player_obj->y;
			bullet_obj->vx = -BULLET_SPEED * 2.0f;
			bullet_obj->vy = 0.0f;
			bullet_obj->a = 90.0f;
			player_back_obj->a = 90.0f;
		}//NORTH
		else if (bullet_direction == NORTH) {
			bullet_obj->direction = bullet_direction;
			bullet_obj->width = 20.0f * power * 0.5f;
			bullet_obj->height = 40.0f;
			bullet_obj->x = player_obj->x;
			bullet_obj->y = player_obj->y - player_back_obj->height / 2 - bullet_obj->height / 2;
			bullet_obj->vx = 0.0f;
			bullet_obj->vy = -BULLET_SPEED * 2.0f;
			bullet_obj->a = 180.0f;
			player_back_obj->a = 180.0f;
		}//SOUTH
		else if (bullet_direction == SOUTH) {
			bullet_obj->direction = bullet_direction;
			bullet_obj->width = 20.0f * power * 0.5f;
			bullet_obj->height = 40.0f;
			bullet_obj->x = player_obj->x;
			bullet_obj->y = player_obj->y + player_back_obj->height / 2 + bullet_obj->height / 2;
			bullet_obj->vx = 0.0f;
			bullet_obj->vy = BULLET_SPEED * 2.0f;
			bullet_obj->a = 0.0f;
			player_back_obj->a = 0.0f;
		}
		return;
	}
	deleteObject(bullet_obj);
}

//플레이어 폭탄 생성함수
void createPlayerBomb()
{
	GameObject* bomb_obj = newObject();

	if (bomb_obj) {
		bomb_obj->type = PLAYER_BOMB;
		bomb_obj->bitmap = createBitmap("bomb.png");

		bomb_obj->direction = SOUTH;
		bomb_obj->width = 100.0f;
		bomb_obj->height = 100.0f;
		bomb_obj->x = WIN_WIDTH / 2;
		bomb_obj->y = WIN_HEIGHT;
		bomb_obj->vx = 0.0f;
		bomb_obj->vy = -10.0f;
		bomb_obj->a = 0.0f;

		return;
	}
	deleteObject(bomb_obj);
}

//적기 총알 생성함수
void createEnemyBullet( GameObject* enemy )
{
	GameObject* bullet_obj = newObject();

	if( bullet_obj )
	{
		bullet_obj->type = ENEMY_BULLET;
		bullet_obj->bitmap = createBitmap("laserRed03.png");
		bullet_obj->width = 10.0f;
		bullet_obj->height = 30.0f;
		if (enemy->direction == EAST) {
			bullet_obj->x = enemy->x - enemy->width / 2 - bullet_obj->width / 2;
			bullet_obj->y = enemy->y;
			bullet_obj->vx = -BULLET_SPEED;
			bullet_obj->vy = 0;
			bullet_obj->a = 90.0f;
		}
		else if(enemy->direction == WEST) {
			bullet_obj->x = enemy->x + enemy->width / 2 + bullet_obj->width / 2;
			bullet_obj->y = enemy->y;
			bullet_obj->vx = BULLET_SPEED;
			bullet_obj->vy = 0;
			bullet_obj->a = 90.0f;
		}
		else if(enemy->direction == NORTH) {
			bullet_obj->x = enemy->x;
			bullet_obj->y = enemy->y + enemy->height / 2 + bullet_obj->width / 2;
			bullet_obj->vx = 0;
			bullet_obj->vy = BULLET_SPEED;
			bullet_obj->a = 0.0f;
		}
		else if(enemy->direction == SOUTH) {
			bullet_obj->x = enemy->x;
			bullet_obj->y = enemy->y - enemy->height / 2 - bullet_obj->width / 2;
			bullet_obj->vx = 0;
			bullet_obj->vy = -BULLET_SPEED;
			bullet_obj->a = 0.0f;
		}
	}
}

//보스 총알 생성함수
void createBossBullet(GameObject* boss) {
	//거리 = 시간*속도 속도 = 거리 / 시간
	//targetX는 패턴 변환시 0으로 초기화
	//BOSS_BULLET
	if (boss) {
		if (boss_pattern == PATTERN1) {
			//왼쪽 중간으로 이동
			GameObject* bullet_obj = newObject();
			if (bullet_obj) {
				//총알은 업데이트 하나당 다른 x로 공격
				//즉, createBossBullet가 호출 될 때마다
				//총알 변위 바꾸어 생성해주면됨.
				//왼쪽->오른쪽 뿌리기 공격
				//오른쪽->왼쪽 뿌리기 공격
				//위의 패턴을 반복

				targetX += dwidth;
		//		printf("targetX : %1.f\n", targetX);
				float width = targetX - boss->x;
				if (targetX >= WIN_WIDTH) {
					if (dwidth > 0) {
						dwidth *= -1;
					}
				}
				if (targetX <= -WIN_WIDTH) {
					if (dwidth < 0) {
						dwidth *= -1;
					}
				}
				bullet_obj->type = ENEMY_BULLET;
				bullet_obj->bitmap = createBitmap("ball.png");
				bullet_obj->width = 20.0f;
				bullet_obj->height = 20.0f;
				bullet_obj->x = boss->x;
				bullet_obj->y = boss->y - boss->height / 2.0f - bullet_obj->height / 2.0f;
				bullet_obj->vx = width / 60;
				bullet_obj->vy = -BULLET_SPEED;
				bullet_obj->a = 180.0f;
			}
		}
		else if (boss_pattern == PATTERN2) {
			//오른쪽 중간으로 이동
			//첫 공격 시 생긴 빈공간에 일정 시간을 두고 쏘는 형태
			float width;
			//dwidth = 53
			targetX += dwidth / 2.0f;
			if (targetX >= dwidth)
				targetX = 0;
			
			if (pattern_time % 15 == 0) {
				float tempTargetX = -dwidth * 5.0f + targetX;
				for (int i = 0; i < (dwidth) / 2; i++) {
					GameObject* bullet_obj = newObject();
					if (bullet_obj) {
						tempTargetX += dwidth;
						width = tempTargetX - boss->x;

						bullet_obj->type = ENEMY_BULLET;
						bullet_obj->bitmap = createBitmap("ball.png");
						bullet_obj->width = 20.0f;
						bullet_obj->height = 20.0f;
						bullet_obj->x = boss->x;
						bullet_obj->y = boss->y - boss->height / 2.0f - bullet_obj->height / 2.0f;
						bullet_obj->vx = width / 60;
						bullet_obj->vy = -BULLET_SPEED + ((float)i / 10);
						bullet_obj->a = 180.0f;
					}
				}
			}
		}
		else if (boss_pattern == PATTERN3) {
			//가운데 위로 이동
			//회오리 모양으로 쏘는형태
			float width;
			//dwidth = 53
			if (pattern_time % 12 == 0) {
				float tempTargetX = -fabs(dwidth) * 5.0f;
				for (int i = 0; i < (fabs(dwidth) / 2); i++) {
					GameObject* bullet_obj = newObject();
					if (bullet_obj) {
						tempTargetX += fabs(dwidth);
						width = tempTargetX - boss->x;

						bullet_obj->type = ENEMY_BULLET;
						bullet_obj->bitmap = createBitmap("ball.png");
						bullet_obj->width = 20.0f;
						bullet_obj->height = 20.0f;
						bullet_obj->x = boss->x;
						bullet_obj->y = boss->y - boss->height / 2.0f - bullet_obj->height / 2.0f;
						bullet_obj->vx = width / 60;
						bullet_obj->vy = -BULLET_SPEED + ((float)i / 10);
						bullet_obj->a = 180.0f;
					}
				}
			}
		}
		else if (boss_pattern == PATTERN4) {
			//중앙으로 이동
			//사방면 순서대로 공격
			//dwidth, dheight = 53
			GameObject* bullet_obj = newObject();
			if (bullet_obj) {
				float width = targetX - boss->x;
				float height = targetY - boss->y;
				//구간 4개로 나누기
				if ((targetX >= WIN_WIDTH) && (targetY <= 0)) {
					if (dheight < 0) {
						dheight *= -1;
					}
				}
				else if ((targetX >= WIN_WIDTH) && (targetY >= WIN_HEIGHT)) {
					if (dwidth > 0) {
						dwidth *= -1;
					}
				}
				else if ((targetX <= 0) && (targetY >= WIN_HEIGHT)) {
					if (dheight > 0) {
						dheight *= -1;
					}
				}
				else if ((targetX <= 0) && (targetY <= -0)) {
					if (dwidth < 0) {
						dwidth *= -1;
					}
				}
				if (((dheight > 0) && ((targetY >= 0) && (targetY < WIN_HEIGHT)))
					|| ((dheight < 0) && ((targetY > 0) && (targetY <= WIN_HEIGHT)))) {
					targetY += dheight;
				}
				else
					targetX += dwidth;

				bullet_obj->type = ENEMY_BULLET;
				bullet_obj->bitmap = createBitmap("ball.png");
				bullet_obj->width = 20.0f;
				bullet_obj->height = 20.0f;
				bullet_obj->x = boss->x;
				bullet_obj->y = boss->y - boss->height / 2.0f - bullet_obj->height / 2.0f;
				bullet_obj->vx = width / 60;
				bullet_obj->vy = height / 60;
				bullet_obj->a = 180.0f;
			}
		}
	}
}

//체력 아이템 생성함수
void createItemHealth()
{
	GameObject* item = newObject();

	if (item)
	{
		item->type = ITEM_HEALTH;
		item->bitmap = createBitmap("health.png");
		item->x = (float)rand() / (float)RAND_MAX * (WIN_WIDTH - 100.0f) + 50.0f;
		item->y = (float)WIN_HEIGHT + 50.0f;
		item->vx = ITEM_POWER_SPEED;
		item->vy = -ITEM_POWER_SPEED;
		item->width = 40.0f;
		item->height = 40.0f;
		item->a = 225.0f;
	}
}

//파워증가 아이템 생성함수
void createItemPower() {
	GameObject* item = newObject();

	if (item) {
		item->type = ITEM_POWER;
		item->bitmap = createBitmap("power.png");
		item->x = (float)rand() / (float)RAND_MAX * (WIN_WIDTH - 100.0f) + 50.0f;
		item->y = (float)WIN_HEIGHT + 50.0f;
		item->vx = ITEM_POWER_SPEED;
		item->vy = -ITEM_POWER_SPEED;
		item->width = 40.0f;
		item->height = 40.0f;
		item->a = 0.0f;
	}
}

//쉴드 아이템 생성함수
void createItemShield()
{
	GameObject* item = newObject();

	if (item)
	{
		item->type = ITEM_SHIELD;
		item->bitmap = createBitmap("shield.png");
		item->x = (float)rand() / (float)RAND_MAX * (WIN_WIDTH - 100.0f) + 50.0f;
		item->y = (float)WIN_HEIGHT + 50.0f;
		item->vx = ITEM_POWER_SPEED;
		item->vy = -ITEM_POWER_SPEED;
		item->width = 40.0f;
		item->height = 40.0f;
		item->a = 225.0f;
	}
}

//죽을 시 파워증가 아이템 반환 함수
void diemotion_createItemPower() {

	for (int i = 1; i < power; power--) {
		GameObject* item = newObject();
		if (item) {
			item->type = ITEM_POWER;
			item->bitmap = createBitmap("power.png");
			item->x = player_obj->x;
			item->y = player_obj->y + player_obj->height / 2.0f;
			item->vx = randomFloat(0,1) + ITEM_POWER_SPEED;
			item->vy = randomFloat(0, 1) + ITEM_POWER_SPEED;
			item->width = 40.0f;
			item->height = 40.0f;
			item->a = 75.0f;
			item->age = 500;
		}
	}
}

//충돌시 파티클 생성함수
void createParticles( GameObject* obj )
{
	int i;
	for( i=0; i < 7; i++ )
	{
		GameObject* particle = newObject();

		if( particle )
		{
			particle->type = PARTICLE;
			particle->bitmap = createBitmap( "particle.png" );
			particle->width = particle->height = randomFloat( 25.0f, 50.0f );
			particle->x = obj->x + randomFloat( -25.0f, +25.0f );
			particle->y = obj->y + randomFloat( -25.0f, +25.0f );
			particle->a = 0.0f;
		}
	}
}

//플레이어 죽을경우 파티클 생성함수
void createDieParticles2( )
{
	int i;
	for (i = 0; i < 10; i++)
	{
		GameObject* particle = newObject();

		if (particle)
		{
			particle->type = PARTICLE;
			particle->bitmap = createBitmap("particle2.png");
			particle->width = WIN_WIDTH / 2.0f;
			particle->height = WIN_HEIGHT / 2.0f;
			particle->x = WIN_WIDTH / 2.0f + randomFloat(-100.0f, + 100.0f);
			particle->y = WIN_HEIGHT / 2.0f + randomFloat(-100.0f, + 100.0f);
			particle->a = 0.0f;
		}
	}
}

//폭탄 터질경우 파티클 생성함수
void createBombparticle()
{
	int i, j;
	for (i = 0; i < 10; i++)
	{
		GameObject* particle = newObject();

		if (particle)
		{
			particle->type = BOMB_PARTICLE;
			particle->bitmap = createBitmap("particle3.png");
			particle->width = WIN_WIDTH / 3.0f;
			particle->height = WIN_HEIGHT / 3.0f;
			particle->x = WIN_WIDTH / 2.0f + randomFloat(-200.0f, +200.0f);
			particle->y = WIN_HEIGHT / 2.0f + randomFloat(-200.0f, +200.0f);
			particle->a = 0.0f;

			for (j = 0; j < POOL_SIZE; j++)
			{
				GameObject* obj = getObject(j);
				if (obj->type == ENEMY1 || obj->type == ENEMY2 || obj->type == ENEMY3
					|| obj->type == ENEMY4 || obj->type == BOSS
					|| obj->type == ENEMY_BULLET || obj->type == GUIDED_BULLET || obj->type == METEOR)
				{

					obj->health -= 4;
					if (obj->health <= 0) {
						if (obj->type == ENEMY1) score += HIT_SCORE;
						else if (obj->type == ENEMY2) score += HIT_SCORE * 2;
						else if (obj->type == ENEMY3) score += HIT_SCORE * 3;
						else if (obj->type == ENEMY4) score += HIT_SCORE * 4;
						deleteObject(obj);
					}
				}
			}

		}
	}
}

//쉴드로 막았을 경우 파티클 생성함수
void createShieldParticles(GameObject* obj)
{
	int i;
	for (i = 0; i < 2; i++) {
		GameObject* shield_particle = newObject();

		if ((shield) && (shield_particle)) {
			shield_particle->type = PARTICLE;
			shield_particle->bitmap = createBitmap("Shield_particle.png");
			if (i == 0) {
				shield_particle->width = shield_particle->height = 120.0f;
				shield_particle->x = player_obj->x;
				shield_particle->y = player_obj->y;
			}
			else {
				shield_particle->width = shield_particle->height = randomFloat(30.0f, 80.0f);
				shield_particle->x = obj->x + randomFloat(-25.0f, +25.0f);
				shield_particle->y = obj->y + randomFloat(-25.0f, +25.0f);
			}
			shield_particle->a = 0.0f;
		}
	}
}

//운석 생성 함수
void createMeteor() {
	GameObject* random_bullet_obj = newObject();

	if (random_bullet_obj)
	{
		random_bullet_obj->type = METEOR;
		random_bullet_obj->width = 40.0f;
		random_bullet_obj->height = 40.0f;

		float randnum = randomFloat(0, 2);
		if (randnum <1) {
			//동쪽 위
			random_bullet_obj->direction = EAST;
			random_bullet_obj->bitmap = createBitmap("meteorBrown.png");
			random_bullet_obj->x = (float)rand() / (float)RAND_MAX * (WIN_WIDTH) + WIN_WIDTH / 2.0f;
			random_bullet_obj->y = (float)WIN_HEIGHT + 50.0f;
			random_bullet_obj->vx = -BULLET_SPEED;
			random_bullet_obj->vy = -BULLET_SPEED;
			random_bullet_obj->a = -90.0f;
		}
		else if ((randnum >= 1) && (randnum <= 2)) {
			//서쪽 위
			random_bullet_obj->direction = WEST;
			random_bullet_obj->bitmap = createBitmap("meteorGray.png");
			random_bullet_obj->x = (float)rand() / (float)RAND_MAX * (WIN_WIDTH) - WIN_WIDTH/2.0f;
			random_bullet_obj->y = (float)WIN_HEIGHT + 50.0f;
			random_bullet_obj->vx = BULLET_SPEED;
			random_bullet_obj->vy = -BULLET_SPEED;
			random_bullet_obj->a = 0.0f;
			
		}
	}
}

//유도 총알 생성함수
void createGuidedBullet() {
	GameObject* random_bullet_obj = newObject();

	if (random_bullet_obj)
	{
		random_bullet_obj->type = GUIDED_BULLET;
		random_bullet_obj->bitmap = createBitmap("laserGreen05.png");
		random_bullet_obj->width = 10.0f;
		random_bullet_obj->height = 30.0f;

		float randnum = randomFloat(0, 4);
		if (randnum <1) {
			random_bullet_obj->direction = EAST;
			random_bullet_obj->x = (float)WIN_WIDTH + 50.0f;
			random_bullet_obj->y = (float)rand() / (float)RAND_MAX * (WIN_HEIGHT - 100.0f) + 50.0f;
			random_bullet_obj->vx = -BULLET_SPEED;
			random_bullet_obj->vy = 0;
			random_bullet_obj->a = 90.0f;
		}
		else if ((randnum >= 1) && (randnum < 2)) {
			random_bullet_obj->direction = WEST;
			random_bullet_obj->x = -50.0f;
			random_bullet_obj->y = (float)rand() / (float)RAND_MAX * (WIN_HEIGHT - 100.0f) + 50.0f;
			random_bullet_obj->vx = BULLET_SPEED;
			random_bullet_obj->vy = 0;
			random_bullet_obj->a = 90.0f;
		}
		else if ((randnum >= 2) && (randnum < 3)) {
			random_bullet_obj->direction = NORTH;
			random_bullet_obj->x = (float)rand() / (float)RAND_MAX * (WIN_WIDTH - 100.0f) + 50.0f;
			random_bullet_obj->y = -50.0f;
			random_bullet_obj->vx = 0;
			random_bullet_obj->vy = BULLET_SPEED;
			random_bullet_obj->a = 0.0f;
		}
		else if ((randnum >= 3) && (randnum < 4)) {
			random_bullet_obj->direction = SOUTH;
			random_bullet_obj->x = (float)rand() / (float)RAND_MAX * (WIN_WIDTH - 100.0f) + 50.0f;
			random_bullet_obj->y = (float)WIN_HEIGHT + 50.0f;
			random_bullet_obj->vx = 0;
			random_bullet_obj->vy = -BULLET_SPEED;
			random_bullet_obj->a = 0.0f;
		}
	}
}

/*----------MOVE FUNCTION----------*/

//동,서,남,북 에 따라 좌표 변경
//vx, vy 는 해당 방향으로의 속도를 뜻하며, createEnemy에 정의되어있다.
void moveEnemy( GameObject* enemy )
{
	if( enemy )
	{
		switch (enemy->type) {
		case ENEMY1:
				enemy->x += enemy->vx;
				enemy->y += enemy->vy;
		case ENEMY2:
		case ENEMY4:
			//EAST, WEST라면 x좌표의 절반, y좌표 전체에 대해 순환하기
			if ((enemy->direction == EAST) || (enemy->direction == WEST)) {
				if (enemy->y - enemy->height / 2 <= 0) {
					if (enemy->vy < 0) {
						enemy->vy *= -1;
					}
				}
				else if (enemy->y + enemy->height / 2 >= WIN_HEIGHT) {
					if (enemy->vy > 0) {
						enemy->vy *= -1;
					}
				}
				// 위는 공통 부분 // 아래는 개인 부분 //
				if (enemy->direction == EAST) {
					if (enemy->x - enemy->width / 2 <= WIN_WIDTH / 2.0f) {
						if (enemy->vx < 0) {
							enemy->vx *= -1;
						}
					}
					else if (enemy->x + enemy->width / 2 >= WIN_HEIGHT) {
						if (enemy->vx > 0) {
							enemy->vx *= -1;
						}
					}
				}
				else if (enemy->direction == WEST) {
					if (enemy->x - enemy->width / 2 <= 0) {
						if (enemy->vx < 0) {
							enemy->vx *= -1;
						}
					}
					else if (enemy->x + enemy->width / 2 >= WIN_WIDTH / 2.0f) {
						if (enemy->vx > 0) {
							enemy->vx *= -1;
						}
					}
				}
			}
			//NORTH, SOUTH라면 x좌표 전체, y좌표의 절반에 대해 순환하기
			else if ((enemy->direction == NORTH) || (enemy->direction == SOUTH)) {
				if (enemy->x - enemy->width / 2 <= 0) {
					if (enemy->vx < 0) {
						enemy->vx *= -1;
					}
				}
				else if (enemy->x + enemy->width / 2 >= WIN_WIDTH) {
					if (enemy->vx > 0) {
						enemy->vx *= -1;
					}
				}
				// 위는 공통 부분 // 아래는 개인 부분 //
				if (enemy->direction == SOUTH) {
					if (enemy->y - enemy->height / 2 <= WIN_HEIGHT / 2.0f) {
						if (enemy->vy < 0) {
							enemy->vy *= -1;
						}
					}
					else if (enemy->y + enemy->height / 2 >= WIN_HEIGHT) {
						if (enemy->vy > 0) {
							enemy->vy *= -1;
						}
					}
				}
				else if (enemy->direction == NORTH) {
					if (enemy->y + enemy->height / 2 >= WIN_HEIGHT / 2.0f) {
						if (enemy->vy > 0) {
							enemy->vy *= -1;
						}
					}
					else if (enemy->y - enemy->height / 2 <= 0) {
						if (enemy->vy < 0) {
							enemy->vy *= -1;
						}
					}
				}
			}
			enemy->x += enemy->vx;
			enemy->y += enemy->vy;
			break;
		case ENEMY3:
			//전체에 대해 순환하기
				if (enemy->x + enemy->width / 2 >= WIN_WIDTH) {
					if (enemy->vx > 0) {
						enemy->vx *= -1;
					}
				}
				else if (enemy->x - enemy->width / 2 <= 0) {
					if (enemy->vx < 0) {
						enemy->vx *= -1;
					}
				}
				if (enemy->y - enemy->height / 2 <= 0) {
					if (enemy->vy < 0) {
						enemy->vy *= -1;
					}
				}
				else if (enemy->y + enemy->height / 2 >= WIN_HEIGHT) {
					if (enemy->vy > 0) {
						enemy->vy *= -1;
					}
				}
			enemy->x += enemy->vx;
			enemy->y += enemy->vy;
			break;
		}
	}

		if ((enemy->direction == EAST) && 
			( (enemy->x + enemy->width / 2 < 0) || (enemy->y < 0) || (enemy->y > WIN_HEIGHT) ))
		{
			deleteObject(enemy);
		}
		else if ((enemy->direction == WEST) && 
			( (enemy->x - enemy->width / 2 > WIN_WIDTH) || (enemy->y < 0) || (enemy->y > WIN_HEIGHT) ))
		{
			deleteObject(enemy);
		}
		else if ((enemy->direction == NORTH) && 
			( (enemy->y - enemy->height / 2 > WIN_HEIGHT) || (enemy->x < 0) || (enemy->x > WIN_WIDTH) ))
		{
			deleteObject(enemy);
		}
		else if ((enemy->direction == SOUTH) && 
			( (enemy->y + enemy->height / 2 < 0) || (enemy->x < 0) || (enemy->x > WIN_WIDTH) ))
		{
			deleteObject(enemy);
		}
}

//보스 움직임 설정
void moveBoss(GameObject* boss) {
	//각 패턴 시간 : 10초
	if (boss) {
		if (boss_pattern == PATTERN1) {
			//왼쪽 중간으로 이동하는  패턴
			float width = 200.0f - boss->x;
			float height = WIN_HEIGHT - 200.0f - boss->y;
			pattern_time -= 1;
			if ( (pattern_time > 0) && ((width != 0) || (height != 0)) && (ready_bullet == 0)) {
				boss->vx = width / (pattern_time - 540);
				boss->vy = height / (pattern_time - 540);
				boss->x += boss->vx;
				boss->y += boss->vy;
			}
			if (((width == 0) && (height == 0))) {
				ready_bullet = 1;
			}
			if (ready_bullet == 1) {
				if (boss->x < 150.0f) {
					boss->vx = (ENEMY_X_SPEED / 5.0f);
				}
				else if (boss->x > 250.0f) {
					boss->vx = -(ENEMY_X_SPEED / 5.0f);
				}
				boss->x += boss->vx;
			}
			if (pattern_time <= 0) {
				pattern_end = 1;
				pattern_time = 600;
			}
		}
		else if (boss_pattern == PATTERN2) {
			//오른쪽 중간으로 올라가는 이동 패턴
			float width = WIN_WIDTH -200.0f - boss->x;
			float height = WIN_HEIGHT - 200.0f - boss->y;
			pattern_time -= 1;
			if ((pattern_time > 0) && ((width != 0) || (height != 0)) && (ready_bullet == 0)) {
				boss->vx = width / (pattern_time - 540);
				boss->vy = height / (pattern_time - 540);
				boss->x += boss->vx;
				boss->y += boss->vy;
			}
			if (((width == 0) && (height == 0))) {
				ready_bullet = 1;
			}
			if (ready_bullet == 1) {
				if (boss->x < 150.0f) {
					boss->vx = (ENEMY_X_SPEED / 5.0f);
				}
				else if (boss->x > 250.0f) {
					boss->vx = -(ENEMY_X_SPEED / 5.0f);
				}
				boss->x += boss->vx;
			}
			if (pattern_time <= 0) {
				pattern_end = 1;
				pattern_time = 600;
			}
		}
		else if (boss_pattern == PATTERN3) {
			//가운데 위으로 이동하는 패턴
			float width = WIN_WIDTH / 2.0f - boss->x;
			float height = WIN_HEIGHT - 100.0f - boss->y;
			pattern_time -= 1;
			if ((pattern_time > 0) && ((width != 0) || (height != 0)) && (ready_bullet == 0)) {
				boss->vx = width / (pattern_time - 540);
				boss->vy = height / (pattern_time - 540);
				boss->x += boss->vx;
				boss->y += boss->vy;
			}

			if (((width == 0) && (height == 0))) {
				ready_bullet = 1;
			}
			if (ready_bullet == 1) {
				if (boss->x < 150.0f) {
					boss->vx = (ENEMY_X_SPEED / 5.0f);
				}
				else if (boss->x > 250.0f) {
					boss->vx = -(ENEMY_X_SPEED / 5.0f);
				}
				boss->x += boss->vx;
			}
			if (pattern_time <= 0) {
				pattern_end = 1;
				pattern_time = 600;
			}
		}
		else if (boss_pattern == PATTERN4) {
			//중앙으로 이동하는 패턴
			float width = WIN_WIDTH / 2.0f - boss->x;
			float height = WIN_HEIGHT / 2.0f - boss->y;
			pattern_time -= 1;
			if ((pattern_time > 0) && ((width != 0) || (height != 0)) && (ready_bullet == 0)) {
				boss->vx = width / (pattern_time - 540);
				boss->vy = height / (pattern_time - 540);
				boss->x += boss->vx;
				boss->y += boss->vy;
			}
			if (((width == 0) && (height == 0))) {
				ready_bullet = 1;
			}
			if (ready_bullet == 1) {
				if (boss->x < 150.0f) {
					boss->vx = (ENEMY_X_SPEED / 5.0f);
				}
				else if (boss->x > 250.0f) {
					boss->vx = -(ENEMY_X_SPEED / 5.0f);
				}
				boss->x += boss->vx;
			}
			if (pattern_time <= 0) {
				pattern_end = 1;
				pattern_time = 600;
			}
		}
	}
}

//플레이어 총알 움직임 설정
void movePlayerBullet( GameObject* bullet )
{
	if( bullet )
	{
		bullet->x += bullet->vx;
		bullet->y += bullet->vy;

		//EAST
		if (bullet->vx < 0) {
			if (bullet->x - bullet->width / 2 < 0.0f)
			{
				deleteObject(bullet);
				player_bullet_count--;
			}
		}//WEST
		else if (bullet->vx > 0) {
			if (bullet->x + bullet->width / 2 > WIN_WIDTH)
			{
				deleteObject(bullet);
				player_bullet_count--;
			}
		}//NORTH
		else if (bullet->vy > 0) {
			if (bullet->y - bullet->height / 2 > WIN_HEIGHT)
			{
				deleteObject(bullet);
				player_bullet_count--;
			}
		}//SOUTH
		else if (bullet->vy < 0) {
			if (bullet->y + bullet->height / 2 < 0.0f)
			{
				deleteObject(bullet);
				player_bullet_count--;
			}
		}
	}
}

//적기 총알 움직임 설정
void moveEnemyBullet( GameObject* bullet )
{
	if( bullet )
	{
		bullet->x += bullet->vx;
		bullet->y += bullet->vy;

		//EAST
		if (bullet->vx < 0) {
			if (bullet->x - bullet->width / 2 < 0.0f)
			{
				deleteObject(bullet);
			}
		}//WEST
		else if (bullet->vx > 0) {
			if (bullet->x + bullet->width / 2 > WIN_WIDTH)
			{
				deleteObject(bullet);
			}
		}//NORTH
		else if (bullet->vy > 0) {
			if (bullet->y - bullet->height / 2 > WIN_HEIGHT)
			{
				deleteObject(bullet);
			}
		}//SOUTH
		else if (bullet->vy < 0) {
			if (bullet->y + bullet->height / 2 < 0.0f)
			{
				deleteObject(bullet);
			}
		}
	}
}

//운석 움직임 설정
void moveMeteor(GameObject* bullet) {

	if (bullet)
	{
		bullet->x += bullet->vx;
		bullet->y += bullet->vy;

		//EAST
		if (bullet->vx < 0) {
			if ((bullet->x - bullet->width / 2 < 0.0f) || (bullet->y - bullet->height / 2 < 0.0f))
			{
				deleteObject(bullet);
			}
		}//WEST
		else if (bullet->vx > 0) {
			if ((bullet->x + bullet->width / 2 > WIN_WIDTH) || (bullet->y - bullet->height / 2 < 0.0f))
			{
				deleteObject(bullet);
			}
		}
	}
}

//유도 총알 움직임 설정
void moveGuidedBullet(GameObject* bullet)
{
	if(!player_obj) {
		deleteObject(bullet);
	}
	if (bullet)
	{
		float width = player_obj->x - bullet->x;
		float height = player_obj->y - bullet->y;

		if (bullet->direction == EAST) {
			if (width > 0)
				width *= -1;
			bullet->vx = -BULLET_SPEED;
			bullet->vy = height / ((width / bullet->vx));
			if (bullet->vy > BULLET_SPEED)
				bullet->vy = BULLET_SPEED;
			else if (bullet->vy < -BULLET_SPEED)
				bullet->vy = -BULLET_SPEED;
		}
		else if (bullet->direction == WEST) {
			if (width < 0)
				width *= -1;
			bullet->vx = BULLET_SPEED;
			bullet->vy = height / ((width / bullet->vx));
			if (bullet->vy > BULLET_SPEED)
				bullet->vy = BULLET_SPEED;
			else if (bullet->vy < -BULLET_SPEED)
				bullet->vy = -BULLET_SPEED;
		}
		else if (bullet->direction == NORTH) {
			if (height < 0)
				height *= -1;
			bullet->vx = width / ((height / bullet->vy));
			if (bullet->vx > BULLET_SPEED)
				bullet->vx = BULLET_SPEED;
			else if (bullet->vx < -BULLET_SPEED)
				bullet->vx = -BULLET_SPEED;
			bullet->vy = BULLET_SPEED;
		}
		else if (bullet->direction == SOUTH) {
			if (height > 0)
				width *= -1;
			bullet->vx = width / ((height / bullet->vy));
			if (bullet->vx > BULLET_SPEED)
				bullet->vx = BULLET_SPEED;
			else if (bullet->vx < -BULLET_SPEED)
				bullet->vx = -BULLET_SPEED;
			bullet->vy = -BULLET_SPEED;
		}
		bullet->x += bullet->vx;
		bullet->y += bullet->vy;

		//EAST
		if (bullet->vx < 0) {
			if (bullet->x - bullet->width / 2 < 0.0f)
			{
				deleteObject(bullet);
			}
		}//WEST
		else if (bullet->vx > 0) {
			if (bullet->x + bullet->width / 2 > WIN_WIDTH)
			{
				deleteObject(bullet);
			}
		}//NORTH
		else if (bullet->vy > 0) {
			if (bullet->y - bullet->height / 2 > WIN_HEIGHT)
			{
				deleteObject(bullet);
			}
		}//SOUTH
		else if (bullet->vy < 0) {
			if (bullet->y + bullet->height / 2 < 0.0f)
			{
				deleteObject(bullet);
			}
		}
	}
}

//체력증가 아이템 움직임 설정
void moveItemHealth(GameObject* item)
{
	if (item->type == ITEM_HEALTH)
	{
		if (item->x - item->width <= 0) {
			if (item->vx < 0) {
				item->vx *= -1;
			}
		}
		else if (item->x + item->width >= WIN_WIDTH) {
			if (item->vx > 0) {
				item->vx *= -1;
			}
		}

		if (item->y - item->height <= 0) {
			if (item->vy < 0) {
				item->vy *= -1;
			}
		}
		else if (item->y + item->height >= WIN_HEIGHT) {
			if (item->vy > 0) {
				item->vy *= -1;
			}
		}
		item->x += item->vx;
		item->y += item->vy;
		item->a += 0.5f;

		if (item->age > 1000)
		{
			deleteObject(item);
		}
	}
}

//파워증가 아이템 움직임 설정
void moveItemPower(GameObject* item)
{
	if (item->type == ITEM_POWER)
	{
		if (item->x - item->width <= 0) {
			if (item->vx < 0) {
				item->vx *= -1;
			}
		}else if (item->x + item->width >= WIN_WIDTH) {
			if (item->vx > 0) {
				item->vx *= -1;
			}
		}

		if (item->y - item->height <= 0) {
			if (item->vy < 0) {
				item->vy *= -1;
			}
		}else if (item->y + item->height >= WIN_HEIGHT) {
			if (item->vy > 0) {
				item->vy *= -1;
			}
		}
		item->x += item->vx;
		item->y += item->vy;
		item->a += 0.5f;

		if (item->age > 1000)
		{
			deleteObject(item);
		}
	}
}

//쉴드생성 아이템 움직임 설정
void moveItemShield(GameObject* item)
{
	if (item->type == ITEM_SHIELD)
	{
		if (item->x - item->width <= 0) {
			if (item->vx < 0) {
				item->vx *= -1;
			}
		}
		else if (item->x + item->width >= WIN_WIDTH) {
			if (item->vx > 0) {
				item->vx *= -1;
			}
		}

		if (item->y - item->height <= 0) {
			if (item->vy < 0) {
				item->vy *= -1;
			}
		}
		else if (item->y + item->height >= WIN_HEIGHT) {
			if (item->vy > 0) {
				item->vy *= -1;
			}
		}
		item->x += item->vx;
		item->y += item->vy;
		item->a += 0.5f;

		if (item->age > 1000)
		{
			deleteObject(item);
		}
	}
}

//충돌시 파티클 움직임 설정
void moveParticle( GameObject* particle )
{
	if( particle )
	{
		particle->x += randomFloat( -5.0f, +5.0f );
		particle->y += randomFloat( -5.0f, +5.0f );
		particle->width *= randomFloat( 0.75f, 0.95f );
		particle->height *= randomFloat( 0.75f, 0.95f );

		if( particle->age > 50 )
		{
			if( randomFloat( 0.0f, 1.0f ) < 0.1f )
			{
				deleteObject( particle );
			}
		}
	}
}

//생성된 폭탄 움직임 설정
void moveBomb(GameObject* bomb)
{
	if (bomb)
	{
		bomb->x += bomb->vx;
		bomb->y += bomb->vy;
		bomb->a += 10.0f;

		if (bomb->y < WIN_HEIGHT / 3.0f)
		{
			deleteObject(bomb);
			createBombparticle(); 
		}

	}
}

//보스 이전 사용자 제자리로 옮기는 움직임 설정
void movePlayer_BeforeBoss(int time) {

	if (player_obj) {
		//생성 위치와 현재 플레이어의 사이 거리 구하기
		float width = WIN_WIDTH / 2.0f - player_obj->x;
		float height = 100 - player_obj->y;
		if (!(width == 0) || !(height == 0)) {
			player_obj->vx = width / time;
			player_obj->vy = height / time;
			player_obj->x += player_obj->vx;
			player_obj->y += player_obj->vy;

			player_back_obj->vx = width / time;
			player_back_obj->vy = height / time;
			player_back_obj->x += player_back_obj->vx;
			player_back_obj->y += player_back_obj->vy;
		}
		//거리 = 시간 * 속도.-> 속도 = 거리/시간
	}
}

/*----------DRAW FUNCTION----------*/

//메인 메뉴 그리기
void drawMenu()
{
	if (title_image)
	{
		drawBitmap(title_image, WIN_WIDTH / 2, WIN_HEIGHT - title_image->height, title_image->width, title_image->height, 0);
	}
	if( button_start )
	{
		drawBitmap(button_start, WIN_WIDTH / 2, WIN_HEIGHT / 2 + button_start->height, button_start->width, button_start->height, 0);

		if (menu_selection == 0)
		{
			setColor(1, 0, 0);
			strokeRectangle(WIN_WIDTH / 2, WIN_HEIGHT / 2 + button_start->height, button_start->width, button_start->height, 0);
		}
	}
	if (button_explain)
	{
		drawBitmap(button_explain, WIN_WIDTH / 2, WIN_HEIGHT / 2 - button_explain->height, button_explain->width, button_explain->height, 0);

		if (menu_selection == 1) 
		{
			setColor(1, 0, 0);
			strokeRectangle(WIN_WIDTH / 2, WIN_HEIGHT / 2 - button_explain->height, button_explain->width, button_explain->height, 0);
		}
	}
	if (button_exit)
	{
		drawBitmap(button_exit, WIN_WIDTH / 2, WIN_HEIGHT / 2 - (button_exit->height) * 3, button_exit->width, button_exit->height, 0);

		if (menu_selection == 2)
		{
			setColor(1, 0, 0);
			strokeRectangle(WIN_WIDTH / 2, WIN_HEIGHT / 2 - (button_exit->height) * 3, button_exit->width, button_exit->height, 0);
		}
	}
}

//사용자 상태 그리기
void drawPlayerStates()
{
	// energy
	setColor(1, 1, 1);
	strokeRectangle(60, WIN_HEIGHT - 70, 100, 30, 0);
	if (player_obj) {
		if (player_obj->health <= 20)
		{
			setColor(1, 0, 0);
		}
		else
		{
			setColor(0, 0, 1);
		}
		fillRectangle(60 - (100 - player_obj->health) / 2, WIN_HEIGHT - 70, player_obj->health - 2, 28, 0);
	}
	// life
	if (num_life != 0) {
		if (num_life >= 1) {
			drawBitmap(life_image1, WIN_WIDTH - 120 + 3 * 30, WIN_HEIGHT - 70, life_image1->width, life_image1->height, 10);
			if (num_life >= 2) {
				drawBitmap(life_image2, WIN_WIDTH - 120 + 2 * 30, WIN_HEIGHT - 70, life_image2->width, life_image2->height, 10);
				if (num_life >= 3) {
					drawBitmap(life_image3, WIN_WIDTH - 120 + 1 * 30, WIN_HEIGHT - 70, life_image3->width, life_image3->height, 10);
				}
			}
		}
	}
	// shield
	char shield_str[15];
	sprintf(shield_str, " x %3d", shield);
	setColor(1, 1, 1);
	drawBitmap(shield_image, WIN_WIDTH - 100, WIN_HEIGHT - 150, shield_image->width, shield_image->height, 0);
	drawText(WIN_WIDTH - 80, WIN_HEIGHT - 160, GLUT_BITMAP_TIMES_ROMAN_24, shield_str);

	// bomb
	char bomb_str[15];
	sprintf(bomb_str, " x %3d", bomb);
	setColor(1, 1, 1);
	drawBitmap(bomb_image, WIN_WIDTH - 100, WIN_HEIGHT - 190, 30, 30, 0);
	drawText(WIN_WIDTH - 80, WIN_HEIGHT - 200, GLUT_BITMAP_TIMES_ROMAN_24, bomb_str);
}

//보스 출현 메시지 그리기
void drawBossDetected() {
	if (boss_detected_image)
	{
		drawBitmap(boss_detected_image, WIN_WIDTH / 2, WIN_HEIGHT / 2, boss_detected_image->width, boss_detected_image->height, 0);
	}
}

//보스 상태 그리기
void drawBossStates() {
	// energy
	if (boss_obj) {
		setColor(1, 1, 1);
		strokeRectangle(60 - (100 - (boss_obj->health / 3.0f)) / 2, WIN_HEIGHT - 30, (boss_obj->health / 3.0f) , 30, 0);
		if (boss_obj->health <= INITIAL_BOSS_ENERGY / 20)
		{
			setColor(1, 0, 0);
		}
		else
		{
			setColor(0, 1, 0);
		}
		fillRectangle(60 - (100 - (boss_obj->health/3.0f)) / 2, WIN_HEIGHT - 30, (boss_obj->health / 3.0f) - 2, 28, 0);
	}
}

//점수, 시간 그리기
void draw_score_Timer() {

	// score
	char score_str[15];
	sprintf(score_str, "Score : %4d", score);
	setColor(1, 1, 1);
	drawText(WIN_WIDTH / 2 - 50, WIN_HEIGHT - 85, GLUT_BITMAP_TIMES_ROMAN_24, score_str);

	//time
	char timer_str[15];
	sprintf(timer_str, "Time : %.2f", play_sec);
	setColor(1, 1, 1);
	drawText(WIN_WIDTH - 140, WIN_HEIGHT - 120, GLUT_BITMAP_TIMES_ROMAN_24, timer_str);

}

//GameOver 그리기
void drawGameOver()
{
	if (ending_image) {
		drawBitmap(ending_image, WIN_WIDTH / 2, WIN_HEIGHT / 2, ending_image->width, ending_image->height, 0);
	}

	char gameover_score_str[20];
	sprintf(gameover_score_str, "Your Score : %d", score);
	setColor(1, 1, 1);
	drawText(WIN_WIDTH / 2 - 50, WIN_HEIGHT / 2 - 120, GLUT_BITMAP_TIMES_ROMAN_24, gameover_score_str);

	char gameover_timer_str[20];
	sprintf(gameover_timer_str, "Your Time : %.2f", play_sec);
	setColor(1, 1, 1);
	drawText(WIN_WIDTH / 2 - 50, WIN_HEIGHT / 2 - 160, GLUT_BITMAP_TIMES_ROMAN_24, gameover_timer_str);
}

//GameClear 그리기
void drawGameClear() {
	if (clear_image) {
		drawBitmap(clear_image, WIN_WIDTH / 2, WIN_HEIGHT / 2, clear_image->width, clear_image->height, 0);
	}

	char gameclear_score_str[20];
	sprintf(gameclear_score_str, "Your Score : %d", score);
	setColor(1, 1, 1);
	drawText(WIN_WIDTH / 2 - 150, WIN_HEIGHT / 2 - 120, GLUT_BITMAP_TIMES_ROMAN_24, gameclear_score_str);

	char gameclear_timer_str[20];
	sprintf(gameclear_timer_str, "Your Time : %.2f", play_sec);
	setColor(1, 1, 1);
	drawText(WIN_WIDTH / 2 - 150, WIN_HEIGHT / 2 - 165, GLUT_BITMAP_TIMES_ROMAN_24, gameclear_timer_str);
}

//How To Use 그리기
void drawExplain() 
{
	if (explain_image)
	{
		drawBitmap(explain_image, WIN_WIDTH / 2, WIN_HEIGHT / 2, explain_image->width, explain_image->height, 0);
	}
}

/*----------ETC FUNCTION----------*/

// 충돌 판정
void checkCollisions()
{
	int i, j;
	for (i = 0; i < POOL_SIZE; i++)
	{
		GameObject* obj1 = getObject(i);

		if ((obj1->type == ENEMY_BULLET) || (obj1->type == METEOR)
			|| (obj1->type == GUIDED_BULLET))
		{
			if (isIntersecting(player_obj, obj1))
			{
				if (shield) {
					createShieldParticles(player_obj);
					shield--;
				}
				else if (obj1->type == METEOR) {
					player_obj->health -= HIT_DAMAGE * 2;
					createParticles(player_obj);
				}
				else {
					player_obj->health -= HIT_DAMAGE;
					createParticles(player_obj);
				}

				deleteObject(obj1);
				if (player_obj->health <= 0)
				{
					num_life--;
					if (life_image3) {
						destroyBitmap(life_image3);
						life_image3 = NULL;
					}
					else if (life_image2) {
						destroyBitmap(life_image2);
						life_image2 = NULL;
					}
					else if (life_image1) {
						destroyBitmap(life_image1);
						life_image1 = NULL;
					}
					if (num_life == 0)
					{
						changeGameMode(POST_GAME);
					}
					else
					{
						if ((game_mode == IN_GAME) || (game_mode == IN_BOSS)) {
							createDieParticles2();
							for (j = 0; j < POOL_SIZE; j++) {
								GameObject* enemybullet = getObject(j);
								if (enemybullet->type == ENEMY_BULLET)
									deleteObject(enemybullet);
							}
							diemotion_createItemPower();
							if (game_mode == IN_GAME)
								changeGameMode(READY_GAME);
							else
								changeGameMode(READY_BOSS);
						}
					}
				}
			}
		}
		if (obj1->type == ITEM_HEALTH)
		{
			if (isIntersecting(player_obj, obj1))
			{
				deleteObject(obj1);

				player_obj->health += ITEM_HEALTH_ENERGY;
				if (player_obj->health > 100)
				{
					score += HIT_SCORE * 5;
					player_obj->health = 100;
				}
			}
		}
		if (obj1->type == ITEM_POWER)
		{
			if (isIntersecting(player_obj, obj1))
			{
				deleteObject(obj1);

				power++;
				if (power > 4)
				{
					score += HIT_SCORE * 10;
					power = 4;
				}
			}
		}
		if (obj1->type == ITEM_SHIELD)
		{
			if (isIntersecting(player_obj, obj1))
			{
				deleteObject(obj1);

				shield++;
			}
		}
		if (obj1->type == PLAYER_BULLET)
		{
			for (j = 0; j < POOL_SIZE; j++)
			{
				GameObject* obj2 = getObject(j);
				if (obj2->type == ENEMY1 || obj2->type == ENEMY2 || obj2->type == ENEMY3
					|| obj2->type == ENEMY4 || obj2->type == BOSS)
				{
					if (isIntersecting(obj1, obj2))
					{
						createParticles(obj2);
						deleteObject(obj1);
						player_bullet_count--;
						obj2->health -= power;
						if (obj2->health <= 0) {
							if (obj2->type == ENEMY1) score += HIT_SCORE;
							else if (obj2->type == ENEMY2) score += HIT_SCORE * 20;
							else if (obj2->type == ENEMY3) score += HIT_SCORE * 30;
							else if (obj2->type == ENEMY4) score += HIT_SCORE * 4;
							else if (obj2->type == BOSS) score += HIT_SCORE * 100;
							deleteObject(obj2);
						}
						if (obj2->type == BOSS) {
							score += HIT_SCORE;
						}
					}
				}
			}
		}

		if (obj1->type == ENEMY1 || obj1->type == ENEMY2 || obj1->type == ENEMY3
			|| obj1->type == ENEMY4 || obj1->type == BOSS) {
			if (isIntersecting(player_obj, obj1))
			{
				if (delay >= HIT_DELAY)
				{
					if (shield) {
						createShieldParticles(player_obj);
						shield--;
					}
					else {
						createParticles(player_obj);
						player_obj->health -= HIT_DAMAGE;
					}

					if (player_obj->health <= 0)
					{
						num_life--;

						if (life_image3) {
							destroyBitmap(life_image3);
							life_image3 = NULL;
						}
						else if (life_image2) {
							destroyBitmap(life_image2);
							life_image2 = NULL;
						}
						else if (life_image1) {
							destroyBitmap(life_image1);
							life_image1 = NULL;
						}

						if (num_life == 0) {
							changeGameMode(POST_GAME);
						}
						else {
							if ((game_mode == IN_GAME) || (game_mode == IN_BOSS)) {
								createDieParticles2();
								for (j = 0; j < POOL_SIZE; j++) {
									GameObject* enemybullet = getObject(j);
									if (enemybullet->type == ENEMY_BULLET)
										deleteObject(enemybullet);
								}
								diemotion_createItemPower();
								if (game_mode == IN_GAME)
									changeGameMode(READY_GAME);
								else
									changeGameMode(READY_BOSS);
							}
						}
					}
					delay = 0;
				}
			}
		}
	}
	delay += 1;
}

//보스 패턴 변화
void changeBossPattern(int pattern) {
	float ranflot = randomFloat(0, 3);
	ready_bullet = 0;
	targetX = 1;
	dwidth = fabs(dwidth);
	switch (boss_pattern) {
	case PATTERN1:
		if (ranflot < 1) boss_pattern = PATTERN2;
		else if (ranflot < 2) boss_pattern = PATTERN3;
		else boss_pattern = PATTERN4;
		break;

	case PATTERN2:
		if (ranflot < 1) boss_pattern = PATTERN1;
		else if (ranflot < 2) boss_pattern = PATTERN3;
		else boss_pattern = PATTERN4;
		break;

	case PATTERN3:
		if (ranflot < 1) boss_pattern = PATTERN1;
		else if (ranflot < 2) boss_pattern = PATTERN2;
		else boss_pattern = PATTERN4;
		break;

	case PATTERN4:
		if (ranflot < 1) boss_pattern = PATTERN1;
		else if (ranflot < 2) boss_pattern = PATTERN2;
		else boss_pattern = PATTERN3;
		break;
	}
}

//게임 모드 변경
void changeGameMode(int new_mode)
{
	// clean up the existing mode
	switch (game_mode)
	{
	case OUT_OF_GAME:
		background = createBitmap("backgroundGray.bmp");
		break;

	case PRE_GAME:
		destroyBitmap(title_image);
		destroyBitmap(button_start);
		destroyBitmap(button_explain);
		destroyBitmap(button_exit);
		title_image = NULL;
		button_start = NULL;
		button_explain = NULL;
		button_exit = NULL;
		break;

	case EXPLAIN_GAME:
		destroyBitmap(explain_image);
		explain_image = NULL;
		break;

	case READY_GAME:
		break;

	case IN_GAME:
		if (new_mode == READY_BOSS) {
			cleanupPoolExceptPlayer();
			boss_obj = NULL;
			ready_count = READY_INTERVAL * 2;
			player_back_obj->a = 0.0f;
			break;
		}
		deleteObject(player_obj);
		deleteObject(player_back_obj);
		player_obj = NULL;
		break;

	case POST_GAME:
		cleanupPool();
		destroyBitmap(ending_image);
		ending_image = NULL;
		break;

	case READY_BOSS:
		break;

	case IN_BOSS:
		if (new_mode == CLEAR_GAME) {
			deleteObject(player_obj);
			deleteObject(player_back_obj);
			player_obj = NULL;
			player_back_obj = NULL;
			boss_obj = NULL;
		}
		break;

	case CLEAR_GAME:
		cleanupPoolExceptPlayer();
		destroyBitmap(clear_image);
		clear_image = NULL;
		break;
	}

	// initialize the new mode
	switch (new_mode)
	{
	case OUT_OF_GAME:
		destroyBitmap(background);
		background = NULL;
		break;

	case PRE_GAME:
		num_life = INITIAL_LIFE;
		play_sec = 1;
		score = 0;
		shield = 0; bomb = 0; prevbomb_count = 0;
		title_image = createBitmap("title.png");
		button_start = createBitmap("button_start.bmp");
		button_explain = createBitmap("button_explain.bmp");
		button_exit = createBitmap("button_exit.bmp");
		menu_selection = 0;
		pattern_time = 0;
		ready_bullet = 0;
		changeBossPattern(boss_pattern);
		break;

	case EXPLAIN_GAME:
		explain_image = createBitmap("explain.png");
		break;

	case READY_GAME:
		ready_count = READY_INTERVAL;
		life_image1 = createBitmap("playerLife3_orange.png");
		life_image2 = createBitmap("playerLife3_orange.png");
		life_image3 = createBitmap("playerLife3_orange.png");
		shield_image = createBitmap("shieldnum.png");
		bomb_image = createBitmap("bomb.png");
		createPlayer();
		break;

	case IN_GAME:
		break;

	case POST_GAME:
		ending_image = createBitmap("gameover.png");
		deleteObject(player_obj);
		deleteObject(player_back_obj);
		player_obj = NULL;
		player_back_obj = NULL;
		break;

	case READY_BOSS:
		if (game_mode == IN_GAME) {
			//보스 출현 warning 메시지 출력.
			boss_detected_image = createBitmap("warning.png");
		}
		ready_count = READY_INTERVAL;
		player_obj->health = INITIAL_ENERGY;
		break;

	case IN_BOSS:
		destroyBitmap(boss_detected_image);
		boss_detected_image = NULL;
		createPlayer();
		createBoss();
		break;

	case CLEAR_GAME:
		clear_image = createBitmap("clear.png");
		break;
	}

	game_mode = new_mode;
}

//적기 개체의 방향 설정
void select_direction(GameObject* enemy_obj) {

	float randnum = randomFloat(0, 4);
	enemy_obj->width = 60.0f;
	enemy_obj->height = 60.0f;

	if (randnum <1) {
		enemy_obj->direction = EAST;
		enemy_obj->x = (float)WIN_WIDTH + 50.0f;
		enemy_obj->y = (float)rand() / (float)RAND_MAX * (WIN_HEIGHT - 100.0f) + 50.0f;
		enemy_obj->a = 270.0f;
		if (enemy_obj->type == ENEMY1) {
			enemy_obj->bitmap = createBitmap("enemyBlack1.png");
			enemy_obj->vx = -ENEMY_X_SPEED - 0.5f;
			enemy_obj->vy = ((float)rand() / (float)RAND_MAX < 0.5f 
				? ENEMY_Y_SPEED : -ENEMY_Y_SPEED);
		}
		else if (enemy_obj->type == ENEMY2) {
			enemy_obj->bitmap = createBitmap("enemyBlack2.png");
			enemy_obj->vx = -ENEMY_X_SPEED;
			enemy_obj->vy = ((float)rand() / (float)RAND_MAX < 0.5f
				? ENEMY_Y_SPEED : -ENEMY_Y_SPEED);
		}
		else if (enemy_obj->type == ENEMY3) {
			enemy_obj->bitmap = createBitmap("enemyBlack3.png");
			enemy_obj->vx = -ENEMY_X_SPEED - 1.5f;
			enemy_obj->vy = ((float)rand() / (float)RAND_MAX < 0.5f
				? ENEMY_Y_SPEED - 0.5f : -ENEMY_Y_SPEED + 0.5f);
		}
		else if (enemy_obj->type == ENEMY4) {
			enemy_obj->bitmap = createBitmap("enemyBlack4.png");
			enemy_obj->vx = -ENEMY_X_SPEED - 1.0f;
			enemy_obj->vy = ((float)rand() / (float)RAND_MAX < 0.5f
				? ENEMY_Y_SPEED + 0.5f : -ENEMY_Y_SPEED - 0.5f);
		}
	}
	else if ((randnum >= 1) && (randnum < 2)) {
		enemy_obj->direction = WEST;
		enemy_obj->x = -50.0f;
		enemy_obj->y = (float)rand() / (float)RAND_MAX * (WIN_HEIGHT - 100.0f) + 50.0f;
		enemy_obj->a = 90.0f;
		if (enemy_obj->type == ENEMY1) {
			enemy_obj->bitmap = createBitmap("enemyBlue1.png");
			enemy_obj->vx = +ENEMY_X_SPEED + 0.5f;
			enemy_obj->vy = ((float)rand() / (float)RAND_MAX < 0.5f
				? ENEMY_Y_SPEED : -ENEMY_Y_SPEED);
		}
		else if (enemy_obj->type == ENEMY2) {
			enemy_obj->bitmap = createBitmap("enemyBlue2.png");
			enemy_obj->vx = +ENEMY_X_SPEED;
			enemy_obj->vy = ((float)rand() / (float)RAND_MAX < 0.5f
				? ENEMY_Y_SPEED : -ENEMY_Y_SPEED);
		}
		else if (enemy_obj->type == ENEMY3) {
			enemy_obj->bitmap = createBitmap("enemyBlue3.png");
			enemy_obj->vx = +ENEMY_X_SPEED + 1.5f;
			enemy_obj->vy = ((float)rand() / (float)RAND_MAX < 0.5f
				? ENEMY_Y_SPEED - 0.5f : -ENEMY_Y_SPEED + 0.5f);
		}
		else if (enemy_obj->type == ENEMY4) {
			enemy_obj->bitmap = createBitmap("enemyBlue4.png");
			enemy_obj->vx = +ENEMY_X_SPEED + 1.0f;
			enemy_obj->vy = ((float)rand() / (float)RAND_MAX < 0.5f
				? ENEMY_Y_SPEED + 0.5f : -ENEMY_Y_SPEED - 0.5f);
		}
	}
	else if ((randnum >= 2) && (randnum < 3)) {
		enemy_obj->direction = NORTH;
		enemy_obj->x = (float)rand() / (float)RAND_MAX * (WIN_WIDTH - 100.0f) + 50.0f;
		enemy_obj->y = -50.0f;
		enemy_obj->a = 180.0f;
		if (enemy_obj->type == ENEMY1) {
			enemy_obj->bitmap = createBitmap("enemyGreen1.png");
			enemy_obj->vx = ((float)rand() / (float)RAND_MAX < 0.5f
				? ENEMY_X_SPEED : -ENEMY_X_SPEED);
			enemy_obj->vy = ENEMY_Y_SPEED + 0.5f;
		}
		else if (enemy_obj->type == ENEMY2) {
			enemy_obj->bitmap = createBitmap("enemyGreen2.png");
			enemy_obj->vx = ((float)rand() / (float)RAND_MAX < 0.5f
				? ENEMY_X_SPEED : -ENEMY_X_SPEED);
			enemy_obj->vy = ENEMY_Y_SPEED;
		}
		else if (enemy_obj->type == ENEMY3) {
			enemy_obj->bitmap = createBitmap("enemyGreen3.png");
			enemy_obj->vx = ((float)rand() / (float)RAND_MAX < 0.5f
				? ENEMY_X_SPEED - 0.5f : -ENEMY_X_SPEED + 0.5f);
			enemy_obj->vy = ENEMY_Y_SPEED + 1.5f;
		}
		else if (enemy_obj->type == ENEMY4) {
			enemy_obj->bitmap = createBitmap("enemyGreen4.png");
			enemy_obj->vx = ((float)rand() / (float)RAND_MAX < 0.5f
				? ENEMY_X_SPEED + 0.5f : -ENEMY_X_SPEED - 0.5f);
			enemy_obj->vy = ENEMY_Y_SPEED + 1.0f;
		}
	}
	else if ((randnum >= 3) && (randnum < 4)) {
		enemy_obj->direction = SOUTH;
		enemy_obj->x = (float)rand() / (float)RAND_MAX * (WIN_WIDTH - 100.0f) + 50.0f;
		enemy_obj->y = (float)WIN_HEIGHT + 50.0f;
		enemy_obj->a = 0.0f;
		if (enemy_obj->type == ENEMY1) {
			enemy_obj->bitmap = createBitmap("enemyRed1.png");
			enemy_obj->vx = ((float)rand() / (float)RAND_MAX < 0.5f
				? ENEMY_X_SPEED : -ENEMY_X_SPEED);
			enemy_obj->vy = -ENEMY_Y_SPEED - 0.5f;
		}
		else if (enemy_obj->type == ENEMY2) {
			enemy_obj->bitmap = createBitmap("enemyRed2.png");
			enemy_obj->vx = ((float)rand() / (float)RAND_MAX < 0.5f
				? ENEMY_X_SPEED : -ENEMY_X_SPEED);
			enemy_obj->vy = -ENEMY_Y_SPEED;
		}
		else if (enemy_obj->type == ENEMY3) {
			enemy_obj->bitmap = createBitmap("enemyRed3.png");
			enemy_obj->vx = ((float)rand() / (float)RAND_MAX < 0.5f
				? ENEMY_X_SPEED - 0.5f : -ENEMY_X_SPEED + 0.5f);
			enemy_obj->vy = -ENEMY_Y_SPEED - 1.5f;
		}
		else if (enemy_obj->type == ENEMY4) {
			enemy_obj->bitmap = createBitmap("enemyRed4.png");
			enemy_obj->vx = ((float)rand() / (float)RAND_MAX < 0.5f
				? ENEMY_X_SPEED + 0.5f : -ENEMY_X_SPEED - 0.5f);
			enemy_obj->vy = -ENEMY_Y_SPEED - 1.0f;
		}
	}
}
#pragma once
#pragma warning(disable:4996)

#define WIN_WIDTH 800
#define WIN_HEIGHT 600

#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#include <float.h>
#include <windows.h>
#include <gl/glut.h>

#define PI	(3.141592)

//
void initVisualApp( unsigned int width, unsigned int height );
void setColor( float r, float g, float b );

void strokeLine( float x0, float y0, float x1, float y1 );
void strokeCircle( float cx, float cy, float r );
void strokeTriangle( float x0, float y0, float x1, float y1, float x2, float y2 );
void strokeRectangle( float cx, float cy, float w, float h, float a );

void fillCircle( float cx, float cy, float r );
void fillTriangle( float x0, float y0, float x1, float y1, float x2, float y2 );
void fillRectangle( float cx, float cy, float w, float h, float a );

void drawText( float x, float y, void* font, char* string );

unsigned int getWinWidth();
unsigned int getWinHeight();

// (1) BITMAP
enum {
	UNKNOWN_FORMAT = -1,
	BMP_FORMAT,
	PNG_FORMAT,
};

typedef struct {
	unsigned int width;
	unsigned int height;
	unsigned int num_channels;
	unsigned char* data;
	unsigned int texture_id;
	int format;
} Bitmap;

Bitmap* createBitmap( char* path );
void drawBitmap( Bitmap* bitmap, float cx, float cy, float sx, float sy, float a );
void destroyBitmap( Bitmap* bitmap );

//개체 방향성
enum {
	EAST,
	WEST,
	SOUTH,
	NORTH,
};

//객체 타입
enum
{
	UNDEFINED = -1,
	PLAYER,
	PLAYER_BACK,
	ENEMY1,
	ENEMY2,
	ENEMY3,
	ENEMY4,
	BOSS,
	PLAYER_BULLET,
	PLAYER_BOMB,
	BOMB_PARTICLE,
	ENEMY_BULLET,
	BOSS_BULLET,
	METEOR,
	GUIDED_BULLET,
	ITEM_HEALTH,
	ITEM_POWER,
	ITEM_SHIELD,
	PARTICLE,
};

// OBJECT POOL
typedef struct
{
	int is_alive;
	int id;
	int type;
	int direction;
	int age;
	int health;

	Bitmap* bitmap;
	float x, y;
	float vx, vy;
	float a;
	float width;
	float height;
} GameObject;

void handleSpecialKey(int key, int x, int y);
void initializePool( int size );
void cleanupPool();
void cleanupPoolExceptPlayer();	//플레이어 제외한 Object Pool 비우기
void finalizePool();

GameObject* newObject();
GameObject* getObject( int i );
void deleteObject( GameObject* obj );

extern GameObject* object_pool;

// (3) HELPER
int isIntersecting( GameObject* obj1, GameObject* obj2 );
float randomFloat( float min, float max );
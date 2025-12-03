import pygame
import sys

# Initialize Pygame
pygame.init()

# Screen Dimensions
SCREEN_WIDTH = 800
SCREEN_HEIGHT = 600
SCREEN = pygame.display.set_mode((SCREEN_WIDTH, SCREEN_HEIGHT))
pygame.display.set_caption("Brick Breaking Tank Game")

# Colors
WHITE = (255, 255, 255)
BLACK = (0, 0, 0)
GREEN = (0, 255, 0)
RED = (255, 0, 0)
BLUE = (0, 0, 255)
YELLOW = (255, 255, 0)

# Game Constants
FPS = 60
TANK_WIDTH = 50
TANK_HEIGHT = 20
TANK_SPEED = 5
BULLET_WIDTH = 5
BULLET_HEIGHT = 10
BULLET_SPEED = 7
BRICK_WIDTH = 75
BRICK_HEIGHT = 20
BRICK_PADDING = 10
BRICK_OFFSET_TOP = 50
BRICK_OFFSET_LEFT = 35

class Tank(pygame.sprite.Sprite):
    def __init__(self):
        super().__init__()
        self.image = pygame.Surface((TANK_WIDTH, TANK_HEIGHT))
        self.image.fill(GREEN)
        self.rect = self.image.get_rect()
        self.rect.centerx = SCREEN_WIDTH // 2
        self.rect.bottom = SCREEN_HEIGHT - 10

    def update(self):
        keys = pygame.key.get_pressed()
        if keys[pygame.K_LEFT] and self.rect.left > 0:
            self.rect.x -= TANK_SPEED
        if keys[pygame.K_RIGHT] and self.rect.right < SCREEN_WIDTH:
            self.rect.x += TANK_SPEED

    def shoot(self):
        return Bullet(self.rect.centerx, self.rect.top)

class Bullet(pygame.sprite.Sprite):
    def __init__(self, x, y):
        super().__init__()
        self.image = pygame.Surface((BULLET_WIDTH, BULLET_HEIGHT))
        self.image.fill(YELLOW)
        self.rect = self.image.get_rect()
        self.rect.centerx = x
        self.rect.bottom = y

    def update(self):
        self.rect.y -= BULLET_SPEED
        # Kill bullet if it goes off screen
        if self.rect.bottom < 0:
            self.kill()

class Brick(pygame.sprite.Sprite):
    def __init__(self, x, y):
        super().__init__()
        self.image = pygame.Surface((BRICK_WIDTH, BRICK_HEIGHT))
        self.image.fill(RED)
        self.rect = self.image.get_rect()
        self.rect.x = x
        self.rect.y = y

def create_bricks():
    bricks = pygame.sprite.Group()
    rows = 5
    cols = 8
    for row in range(rows):
        for col in range(cols):
            x = BRICK_OFFSET_LEFT + col * (BRICK_WIDTH + BRICK_PADDING)
            y = BRICK_OFFSET_TOP + row * (BRICK_HEIGHT + BRICK_PADDING)
            brick = Brick(x, y)
            bricks.add(brick)
    return bricks

def main():
    clock = pygame.time.Clock()
    font = pygame.font.SysFont(None, 55)

    while True:
        # Game Reset
        all_sprites = pygame.sprite.Group()
        bullets = pygame.sprite.Group()

        tank = Tank()
        all_sprites.add(tank)

        bricks = create_bricks()
        all_sprites.add(bricks)

        game_active = True
        game_over = False
        won = False

        while game_active:
            # Event Handling
            for event in pygame.event.get():
                if event.type == pygame.QUIT:
                    pygame.quit()
                    sys.exit()
                elif event.type == pygame.KEYDOWN:
                    if event.key == pygame.K_SPACE and not game_over:
                        bullet = tank.shoot()
                        all_sprites.add(bullet)
                        bullets.add(bullet)
                    elif event.key == pygame.K_r and game_over:
                        game_active = False # Break inner loop to restart

            if not game_over:
                # Updates
                all_sprites.update()

                # Collision: Bullets vs Bricks
                hits = pygame.sprite.groupcollide(bullets, bricks, True, True)

                # Check Win Condition
                if len(bricks) == 0:
                    game_over = True
                    won = True

            # Draw
            SCREEN.fill(BLACK)
            all_sprites.draw(SCREEN)

            if game_over:
                if won:
                    text = font.render("YOU WIN! Press R to Restart", True, WHITE)
                else:
                    text = font.render("GAME OVER", True, WHITE)

                text_rect = text.get_rect(center=(SCREEN_WIDTH/2, SCREEN_HEIGHT/2))
                SCREEN.blit(text, text_rect)

            pygame.display.flip()
            clock.tick(FPS)

if __name__ == "__main__":
    main()

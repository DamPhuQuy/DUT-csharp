// package com.example;

// import java.awt.Color;
// import java.awt.Graphics;
// import java.awt.image.BufferedImage;
// import java.util.Random;

// import javax.swing.JFrame;

// import lombok.AllArgsConstructor;
// import lombok.Getter;
// import lombok.NoArgsConstructor;
// import lombok.Setter;

// public class BanBia extends JFrame {
//     static int w = 600, h = 400; // Tăng kích thước bàn một chút
//     int off = 50;
//     Ball bs[];
//     BufferedImage img;
//     Graphics g;
//     Random rand = new Random();

//     public BanBia() {
//         this.setTitle("Ban BiA Vật Lý");
//         this.setSize(w + off * 2, h + off * 2);
//         this.setResizable(false);
//         img = new BufferedImage(w + off * 2, h + off * 2, BufferedImage.TYPE_INT_ARGB);
//         g = img.getGraphics();
//         this.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);

//         bs = new Ball[10]; // Thêm nhiều bóng hơn cho vui
//         for (int i = 0; i < bs.length; i++) {
//             // Khởi tạo vị trí ngẫu nhiên không đè lên nhau
//             bs[i] = new Ball(
//                 100 + rand.nextInt(400),
//                 100 + rand.nextInt(200),
//                 15,
//                 (rand.nextDouble() - 0.5) * 10, // Vận tốc x mạnh hơn
//                 (rand.nextDouble() - 0.5) * 10, // Vận tốc y mạnh hơn
//                 0.998
//             );
//             bs[i].start();
//         }
//         this.setVisible(true);
//     }

//     // Logic xử lý va chạm giữa các quả bóng
//     public void checkCollisions() {
//         for (int i = 0; i < bs.length; i++) {
//             for (int j = i + 1; j < bs.length; j++) {
//                 Ball b1 = bs[i];
//                 Ball b2 = bs[j];

//                 double dx = b2.getX() - b1.getX();
//                 double dy = b2.getY() - b1.getY();
//                 double dist = Math.sqrt(dx * dx + dy * dy);
//                 double minDist = b1.getR() + b2.getR();

//                 if (dist < minDist) {
//                     // 1. Xử lý chồng lấp (Static Resolution)
//                     double overlap = 0.5 * (minDist - dist);
//                     double nx = dx / dist;
//                     double ny = dy / dist;

//                     b1.setX(b1.getX() - overlap * nx);
//                     b1.setY(b1.getY() - overlap * ny);
//                     b2.setX(b2.getX() + overlap * nx);
//                     b2.setY(b2.getY() + overlap * ny);

//                     // 2. Phản xạ vận tốc (Elastic Collision)
//                     // Tính tích vô hướng vận tốc tương đối và vector pháp tuyến
//                     double p = (b1.getVx() * nx + b1.getVy() * ny - b2.getVx() * nx - b2.getVy() * ny);

//                     b1.setVx(b1.getVx() - p * nx);
//                     b1.setVy(b1.getVy() - p * ny);
//                     b2.setVx(b2.getVx() + p * nx);
//                     b2.setVy(b2.getVy() + p * ny);
//                 }
//             }
//         }
//     }

//     @Override
//     public void paint(Graphics g1) {
//         // Xử lý vật lý trước khi vẽ
//         checkCollisions();

//         // Vẽ nền bàn bida
//         g.setColor(new Color(30, 130, 70)); // Màu xanh lá cây bida
//         g.fillRect(0, 0, getWidth(), getHeight());

//         g.setColor(new Color(139, 69, 19)); // Màu nâu thành bàn
//         g.fillRect(off - 10, off - 10, w + 20, h + 20);

//         g.setColor(new Color(30, 130, 70));
//         g.fillRect(off, off, w, h);

//         // Vẽ bóng
//         for (Ball b : bs) {
//             int x = (int)b.getX() + off;
//             int y = (int)b.getY() + off;
//             int r = (int)b.getR();

//             // Đổ bóng cho đẹp
//             g.setColor(new Color(0, 0, 0, 50));
//             g.fillOval(x - r + 3, y - r + 3, r * 2, r * 2);

//             g.setColor(Color.YELLOW);
//             g.fillOval(x - r, y - r, r * 2, r * 2);
//             g.setColor(Color.BLACK);
//             g.drawOval(x - r, y - r, r * 2, r * 2);
//         }

//         g1.drawImage(img, 0, 0, null);
//         repaint(); // Vòng lặp vẽ liên tục
//     }

//     public static void main(String[] args) {
//         new BanBia();
//     }
// }

// @Getter
// @Setter
// @NoArgsConstructor
// @AllArgsConstructor
// class Ball extends Thread {
//     private double x, y, r, vx, vy;
//     private double friction = 0.998; // Ma sát nhẹ để bóng lăn lâu hơn

//     @Override
//     public void run() {
//         while (true) {
//             // 1. Cập nhật vị trí
//             x += vx;
//             y += vy;

//             // 2. Ma sát (Chỉ giảm khi đang di chuyển)
//             vx *= friction;
//             vy *= friction;

//             // Dừng hẳn nếu quá chậm
//             if (Math.abs(vx) < 0.01) vx = 0;
//             if (Math.abs(vy) < 0.01) vy = 0;

//             // 3. Va chạm tường (Sử dụng static W, H từ BanBia)
//             if (x - r < 0) { x = r; vx = -vx; }
//             if (x + r > BanBia.w) { x = BanBia.w - r; vx = -vx; }
//             if (y - r < 0) { y = r; vy = -vy; }
//             if (y + r > BanBia.h) { y = BanBia.h - r; vy = -vy; }

//             try {
//                 Thread.sleep(10); // 100 FPS cho chuyển động mượt
//             } catch (InterruptedException e) {
//                 break;
//             }
//         }
//     }
// }


package com.example;

import java.awt.Color;
import java.awt.Font;
import java.awt.Graphics;
import java.awt.Graphics2D;
import java.awt.Point;
import java.awt.image.BufferedImage;
import java.util.Random;

import javax.swing.JFrame;

public class BanBia extends JFrame {

    static int w = 500, h = 300;
    int off = 30;

    static Ball[] balls;

    BufferedImage img;
    Graphics2D g;
    Random rand = new Random();

    // 6 lỗ
    int holeR = 25;
    Point[] holes = {
            new Point(0, 0),
            new Point(w / 2, 0),
            new Point(w, 0),
            new Point(0, h),
            new Point(w / 2, h),
            new Point(w, h)
    };

    public BanBia() {

        this.setTitle("Ban BiA");
        this.setSize(w + off * 2, h + off * 2);
        this.setDefaultCloseOperation(EXIT_ON_CLOSE);

        img = new BufferedImage(w + off * 2, h + off * 2,
                BufferedImage.TYPE_INT_ARGB);
        g = img.createGraphics();

        balls = new Ball[10];

        for (int i = 0; i < balls.length; i++) {
            balls[i] = new Ball(
                    100 + i * 30,
                    150,
                    15,
                    rand.nextDouble() * 4 - 2,
                    rand.nextDouble() * 4 - 2,
                    i + 1 // số trên bi
            );
        }

        for (Ball b : balls)
            b.start();

        this.setVisible(true);
    }

    public static void main(String[] args) {
        new BanBia();
    }

    public void paint(Graphics g1) {

        g.setColor(Color.WHITE);
        g.fillRect(0, 0, getWidth(), getHeight());

        g.setColor(new Color(0, 120, 0));
        g.fillRect(off, off, w, h);

        // vẽ lỗ
        g.setColor(Color.BLACK);
        for (Point p : holes) {
            g.fillOval(p.x + off - holeR,
                    p.y + off - holeR,
                    holeR * 2,
                    holeR * 2);
        }

        g.setColor(Color.BLACK);
        g.drawRect(off, off, w, h);

        // vẽ bi
        for (Ball b : balls) {

            if (!b.active) continue;

            int x = (int) b.x + off;
            int y = (int) b.y + off;
            int r = (int) b.r;

            g.setColor(Color.YELLOW);
            g.fillOval(x - r, y - r, r * 2, r * 2);

            g.setColor(Color.BLACK);
            g.drawOval(x - r, y - r, r * 2, r * 2);

            // Vẽ số
            g.setColor(Color.BLACK);
            g.setFont(new Font("Arial", Font.BOLD, 12));
            g.drawString("" + b.number, x - 4, y + 4);
        }

        g1.drawImage(img, 0, 0, null);
        repaint();
    }
}

class Ball extends Thread {

    double x, y;
    double r;
    double vx, vy;
    int number;
    boolean active = true;

    public Ball(double x, double y, double r,
                double vx, double vy,
                int number) {
        this.x = x;
        this.y = y;
        this.r = r;
        this.vx = vx;
        this.vy = vy;
        this.number = number;
    }

    public void run() {

        double friction = 0.999;

        while (true) {
if (!active) continue;

            x += vx;
            y += vy;

            vx *= friction;
            vy *= friction;

            if (Math.abs(vx) < 0.01) vx = 0;
            if (Math.abs(vy) < 0.01) vy = 0;

            // bật tường
            if (x - r < 0 && vx < 0) vx = -vx;
            if (x + r >= BanBia.w && vx > 0) vx = -vx;
            if (y - r < 0 && vy < 0) vy = -vy;
            if (y + r >= BanBia.h && vy > 0) vy = -vy;

            // va chạm bi
            for (Ball other : BanBia.balls) {

                if (other == this || !other.active) continue;

                double dx = other.x - this.x;
                double dy = other.y - this.y;
                double dist = Math.sqrt(dx * dx + dy * dy);

                if (dist < this.r + other.r) {

                    double overlap = (this.r + other.r - dist) / 2;
                    double nx = dx / dist;
                    double ny = dy / dist;

                    this.x -= overlap * nx;
                    this.y -= overlap * ny;
                    other.x += overlap * nx;
                    other.y += overlap * ny;

                    double tempVx = this.vx;
                    double tempVy = this.vy;

                    this.vx = other.vx;
                    this.vy = other.vy;
                    other.vx = tempVx;
                    other.vy = tempVy;
                }
            }

            // kiểm tra rơi lỗ
            for (Point hole : new BanBia().holes) {

                double dx = hole.x - x;
                double dy = hole.y - y;
                double dist = Math.sqrt(dx * dx + dy * dy);

                if (dist < 25) {
                    active = false;
                    vx = 0;
                    vy = 0;
                }
            }

            try {
                Thread.sleep(1);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
    }
}

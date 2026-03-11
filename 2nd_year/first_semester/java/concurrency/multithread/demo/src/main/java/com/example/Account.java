package com.example;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
public class Account {
    private int money;
    private String name;

    public synchronized void withdraw(int desiredMoney) {
        if (money >= desiredMoney) {
            money -= desiredMoney;
            System.out.println("Withdraw " + desiredMoney + " successfully. Remaining money: " + money);
        } else {
            System.out.println("Not enough money to withdraw. Remaining money: " + money);
        }
    }
}

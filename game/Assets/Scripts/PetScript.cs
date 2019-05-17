﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PetScript : MonoBehaviour
{
    public TextMeshProUGUI winText;

    private PetController petController;
    private float health;
    void Start()
    {
        winText.SetText("");
        petController = FindObjectOfType<PetController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            other.gameObject.SetActive(false);
            int foodValueIndex = other.gameObject.GetComponent<FoodScript>().valueIndex;
            petController.questions[petController.currentProblem]["studentSelection"] = foodValueIndex;
            if (foodValueIndex == petController.correctSolutionIndex) {
                petController.correctAnswerCount++;
                petController.questions[petController.currentProblem]["correct"] = true;
                petController.totalQuestions.text = petController.correctAnswerCount + "/" + petController.totalQuestionCount;
                if (petController.lastProblem)
                {
                    winText.SetText("Game Finished!\nTime: " + petController.currentTimerText + "\nCorrect Answers: " + petController.totalQuestions.text);
                    petController.timerGoing = false;
                    petController.timerText.gameObject.SetActive(false);
                    petController.wonGame = true;
                    NotificationsManager.ScheduleNotifcation("Your pet is getting hungry!", "Tap to start answering questions to feed them!", 5, 1440); // 1440 mintues = 24 hours
                    petController.saveAttempt();
                }
                else
                {
                    petController.nextQuestion();
                }
            }
            else
            {
                petController.nextQuestion();
                //health = petController.OnDamage();
                //if (health == 0.0f)
                //{
                //    winText.SetText("Game Over!\nTime: " + petController.currentTimerText + "/nCorrect Answers:" + petController.totalQuestions);
                //    petController.timerGoing = false;
                //    petController.timerText.gameObject.SetActive(false);
                //    petController.wonGame = true;
                //}
            }
                
        }
    }
}

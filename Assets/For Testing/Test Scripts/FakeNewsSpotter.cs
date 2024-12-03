using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FakeNewsSpotter : MonoBehaviour
{
    // The class to hold email data
    public class Email
    {
        public string EmailAddress;
        public string Description;
        public bool IsSpam;

        // Constructor to set the email's properties
        public Email(string emailAddress, string description, bool isSpam)
        {
            EmailAddress = emailAddress;
            Description = description;
            IsSpam = isSpam;
        }
    }

    // List of emails
    private List<Email> emails = new List<Email>
{
    new Email("abc.213@example.com",
              "You've won $1,000,000! Click here to claim your prize. Don't miss out on this amazing opportunity! To claim your prize, just click the link below and follow the instructions on the page. (http://www.fakeprizeclaim.com)",
              true),

    new Email("noreply@amazon.com",
              "Your Amazon order has been shipped and is on its way. Track your shipment using the provided link. You can follow the package's progress in real time. (https://www.amazon.com/track-your-order)",
              false),

    new Email("support@bankingservice.com",
              "Your account needs verification. Please verify your account to continue using our services. You must verify your identity to ensure your security and avoid any service interruptions. [Verify Now](http://www.fakesite.com/verify-account)",
              false),

    new Email("congrats@loanservice.com",
              "Congratulations! You've been selected for a loan. Click to claim your loan approval and funding. This loan can help you achieve your goals, whether it's buying a new car or renovating your home. (http://www.fakelending.com/loan-offer)",
              true),

    new Email("info@technews.com",
              "Breaking News: New iPhone model released today! Pre-order yours now. This exciting new release features enhanced camera technology and performance upgrades. Don’t miss out on being one of the first to own it. (https://www.apple.com/iphone-preorder)",
              false),

    new Email("newsletter@fitnessguru.com",
              "Get a free 30-day fitness plan when you sign up today! Don't miss out! This plan is tailored to help you reach your fitness goals, whether you're looking to build muscle, lose weight, or increase stamina. (https://www.fitnessguru.com/start)",
              true),

    new Email("alert@paypal.com",
              "Suspicious login detected on your PayPal account. Please verify your identity immediately. This is a critical security alert to prevent unauthorized access to your account. Click the link below to secure your account. (https://www.paypal.com/verify-account)",
              true),

    new Email("winners@lottery.com",
              "Congratulations! You've won a free trip to Hawaii! Click here to claim your prize. The trip includes flights, accommodation, and a guided tour of the beautiful island. (http://www.fakelottery.com/win-trip)",
              false),

    new Email("no-reply@socialmedia.com",
              "A new post was tagged with your name. Check it out! You’ve been mentioned in an exciting new post that’s gathering a lot of attention. Make sure to check it out and see what others are saying. (https://www.socialmedia.com/your-post)",
              true),

    new Email("info@thegooddeal.com",
              "Huge clearance sale! 70% off on all electronics. Limited time offer. Shop the best deals on electronics, from smartphones to laptops, while supplies last. Don’t miss out on these incredible discounts! (http://www.fakestore.com/sale)",
              false),

    new Email("contact@movieupdates.com",
              "Pre-order your tickets for the highly anticipated movie premiere next week! This movie is expected to be a blockbuster, and tickets are selling fast. Be sure to secure your spot at the premiere. (https://www.movieupdates.com/tickets)",
              true),

    new Email("urgent@bankingservice.com",
              "Your account has been temporarily locked due to suspicious activity. Click the link to restore access. We’ve detected unusual transactions in your account. For your protection, please verify your identity to restore access. (http://www.fakesite.com/restore-account)",
              false),

    new Email("noreply@apple.com",
              "Your Apple ID has been successfully updated. If this was not you, please contact support. For added security, make sure to review all devices connected to your Apple ID. (https://www.apple.com/support)",
              true),

    new Email("offer@onlineshopping.com",
              "Flash sale: Up to 50% off on all your favorite brands. Shop now! From fashion to electronics, take advantage of the massive savings while stocks last. This sale is happening for a limited time only. (https://www.onlineshopping.com/flash-sale)",
              false),

    new Email("customer-service@bookstore.com",
              "Your book order is on its way! Here's your tracking number. The package should arrive soon, and you’ll be able to start reading your new books in no time. (https://www.bookstore.com/track-order)",
              true),

    new Email("alerts@phishingalert.com",
              "Important alert! Your account has been compromised. Click here to secure it. We recommend acting quickly to prevent further unauthorized access. Click the link below to restore your account’s security. (http://www.fakephishing.com/secure)",
              false),

    new Email("admin@taxrefund.com",
              "You are eligible for a tax refund of $500. Click here to claim your refund. This is a limited time offer, so make sure to claim your refund before the deadline passes. (http://www.fakerefund.com/claim)",
              false),

    new Email("service@onlinegames.com",
              "Your gaming account has been upgraded. Check your new perks! With this upgrade, you'll enjoy access to premium features, including exclusive game modes and bonus items. (https://www.onlinegames.com/my-account)",
              true),

    new Email("security@cloudstorage.com",
              "Your cloud storage account was accessed from a new device. If this was not you, click here to secure your account. Your data may be at risk, so please take action to protect your information. (https://www.cloudstorage.com/secure-account)",
              true),

    new Email("no-reply@bankingapp.com",
              "Your recent bank transfer has been successfully completed. View details of the transaction by clicking the link below. If this was not you, please report it immediately. (https://www.bankingapp.com/transaction)",
              true),

    new Email("contact@streamingservice.com",
              "Your subscription to Streaming Service is about to expire. Renew now to continue enjoying unlimited content. Renewing your subscription ensures uninterrupted access to your favorite shows and movies. (https://www.streamingservice.com/renew)",
              false),

    new Email("newdeals@discountstore.com",
              "Big discounts on your favorite items. Check out the latest deals! Whether you’re looking for gadgets, apparel, or home goods, we have amazing deals just for you. (https://www.discountstore.com/deals)",
              true)
};


    // UI elements
    public TextMeshProUGUI emailText; // To display the email message
    public TextMeshProUGUI scoreText; // To display the player's score
    public TextMeshProUGUI resultText; // To show the final result (win/lose)
    public UnityEngine.UI.Button spamButton; // Button to mark as spam
    public UnityEngine.UI.Button legitButton; // Button to mark as legit

    private int score = 0;
    private int totalEmails = 0;
    private Email currentEmail;

    void Start()
    {
        // Set button listeners for sorting emails
        spamButton.onClick.AddListener(() => CheckAnswer(true)); // Spam button
        legitButton.onClick.AddListener(() => CheckAnswer(false)); // Legit button

        // Start the game
        ShowNextEmail();
    }

    void ShowNextEmail()
    {
        if (emails.Count > 0)
        {
            int index = Random.Range(0, emails.Count); // Pick a random email
            currentEmail = emails[index];
            emailText.text = "Email: " + currentEmail.EmailAddress + "\n\n" + currentEmail.Description; // Display the email message

            QuestsManager.questsManager.AddQuestItem("Identify if spam email or legit email", 1);

            emails.RemoveAt(index); // Remove it from the list to prevent repetition

            

        }
        else
        {
            EndGame(); // End the game when all emails have been processed
        }


    }

    void CheckAnswer(bool isSpam)
    {
        // Check if the player's answer matches the email type
        if (currentEmail.IsSpam == isSpam)
        {
            score++; // Increment score if correct
        }

        totalEmails++; // Increment total emails processed
        scoreText.text = "SCORE: " + score + "/" + totalEmails; // Update score display

        ShowNextEmail(); // Show the next email
    }

    void EndGame()
    {
        // Disable buttons and show the final result
        spamButton.interactable = false;
        legitButton.interactable = false;

        resultText.text = "Game Over! Your score: " + score + "/" + totalEmails;
    }
}

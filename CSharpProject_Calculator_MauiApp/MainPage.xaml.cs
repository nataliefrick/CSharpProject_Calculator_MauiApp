namespace CSharpProject_Calculator_MauiApp;

public partial class MainPage : ContentPage
{


	public MainPage()
	{
		InitializeComponent();
        ClearEqn(this, null);
    }

    int currentState = 1;
    string mathOperator;
    double firstNr, secondNr;

    // Capture the numbers pushed on the calculator
    void CaptureNr(object sender, EventArgs e)
    {
        this.equation.FontSize = 18; //return font size to normal
        Button button = (Button)sender;
        string pressed = button.Text;
  

        // if both the first number and math operator (currentState = -2) has been assigned, or equals button has been pressed
        if (this.answer.Text == "" || currentState < 0)
        {
            this.answer.Text = string.Empty;

            // if the equals btn is pressed (currentState = -1), empty equation field and set currentState = 1
            if (currentState == -1)
            {
                this.equation.Text = string.Empty;
                currentState *= -1;
            }

            // if first nr and mathoperator have been assigned, still needing second nr.: currentState = -2
            if (currentState == -2)
                currentState *= -1;
        }

        this.equation.Text += pressed;
        this.answer.Text += pressed;
        

        if (double.TryParse(this.answer.Text, out double tempNumber))
        {
            if (currentState == 1)
            // first number has not been set
            {
                firstNr = tempNumber;
            }
            else // first nr has been set, now set nr2
            {
                secondNr = tempNumber;

            }
        }
    }

    // Capture the mathematical operation pushed on the calculator
    void CaptureOperation(object sender, EventArgs e)
    {
        // If this is a second operator, sum up the first part of the equation then continue.
        // This unfortunately does not support proper order of operations, which will not result in an accurate answer.

        if (currentState == 2)
        {
            OnEquals(this, null);
        }

        currentState = -2;
        Button button = (Button)sender;
        string pressed = button.Text;
        mathOperator = pressed.ToLower();
        this.equation.Text += pressed.ToLower();

    }

    // function that runs on push of equals btn
    void OnEquals(object sender, EventArgs e)
    {
        if (currentState == 2) // meaning both first, second and operator is set.
        {
            // this.answer.Text = firstNr.ToString() + mathOperator + secondNr.ToString();
            var result = DoMath.DoCalculation(firstNr, secondNr, mathOperator);

            this.answer.Text = result.ToString();
            firstNr = result;
            currentState = -1; // change state to -1 to initiate reset upon pushing of a new nr btn (captures the case that equalsbtn not pushed)
        }

        this.equation.Text = this.answer.Text;
        this.equation.FontSize = 28; // increase font size to show answer
        this.answer.Text = "";
    }

    // function that runs when clear btn pressed. All values set to 0 and currentState=1
    void ClearEqn(object sender, EventArgs e)
    {
        firstNr = 0;
        secondNr = 0;
        currentState = 1;
        this.answer.Text = "";
        this.equation.Text = "";
        this.equation.FontSize = 18;
    }

    // fnc that is run if percentage btn is pushed. Divides nr by 100.
    void OnPercentage(object sender, EventArgs e)
    {
        if (firstNr == 0)
            return;
        double percResult = firstNr / 100;
        this.equation.Text = firstNr.ToString() + "%";
        this.answer.Text = "0";
        this.answer.Text += percResult.ToString("#.######"); // this causes result to drop unneccessary trailing 0s
        firstNr = percResult;
        currentState = -2;
        mathOperator = "x";
    }

    //fnc that runs on power 2 btn pushed
    void OnPower2(object sender, EventArgs e)
    {
        if (firstNr == 0)
            return;

        this.equation.Text = firstNr + "^2";
        firstNr *= firstNr;
        this.answer.Text = firstNr.ToString();
    }



    
}


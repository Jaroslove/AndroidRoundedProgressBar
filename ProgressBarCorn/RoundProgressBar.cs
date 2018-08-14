using System;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Util;
using Android.Views;
using static Android.Graphics.Paint;

namespace ProgressBarCorn
{
    public class RoundProgressBar : View
    {
        private static int DEFAULT_BACKGROUND_COLOR = 0x00009688;

        private static int DEFAULT_PROGRESS_COLOR = 16750335;

        private Path mHorizontalBgPath;

        private Paint mHorizontalBgPaint;

        private Path mProgressPath;

        private Paint mProgressPaint;

        private long mProgress = 0;

        private long mMax = 100;

        private long mProgressBarWidth;

        private int mPathViewHeight = 0;

        private int mInitMoveToX = 0;

        private int mInitMoveToY = 0;

        private int mDefaultRoundConrnerWidth;

        private int mRoundCornerBackgroundColor;

        private int mRoundCornerProgressColor;

        public RoundProgressBar(Context context, IAttributeSet attributeSet):
            base(context, attributeSet)
        {
            TypedArray attributes = context.ObtainStyledAttributes
                                           (attributeSet, Resource.Styleable.RoundCornerProgressBar);
            mDefaultRoundConrnerWidth = attributes
                    .GetDimensionPixelSize(Resource.Styleable.RoundCornerProgressBar_round_corner_width, 
                                           Dip2px(Context, 4));
            mRoundCornerBackgroundColor = attributes
                    .GetColor(Resource.Styleable.RoundCornerProgressBar_round_corner_background_color, DEFAULT_BACKGROUND_COLOR);
            mRoundCornerProgressColor = attributes.
                      GetColor(Resource.Styleable.RoundCornerProgressBar_round_corner_progress_color,
                    DEFAULT_PROGRESS_COLOR);
            attributes.Recycle();
            Init();
        }

        public RoundProgressBar(Context context): base(context)
        {
            mDefaultRoundConrnerWidth = Dip2px(Context, 4);
            Init();
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
            mInitMoveToX = Dip2px(Context, 7);
            mInitMoveToY = mInitMoveToX;
            int dw = MeasuredWidth + PaddingLeft + PaddingRight;
            int dh = mPathViewHeight + PaddingTop + PaddingBottom;
            mInitMoveToX = mInitMoveToX + PaddingLeft;
            mInitMoveToY = mInitMoveToY + PaddingTop;
            mProgressBarWidth = MeasuredWidth - (PaddingRight + Dip2px(Context, 7));
            SetMeasuredDimension(ResolveSizeAndState(dw, widthMeasureSpec, 0),
                    ResolveSizeAndState(dh, heightMeasureSpec, 0));
        }

        void Init()
        {
            mHorizontalBgPath = new Path();
            mProgressPath = new Path();
            mHorizontalBgPaint = new Paint(PaintFlags.AntiAlias);
            mHorizontalBgPaint.StrokeWidth = mDefaultRoundConrnerWidth;
            mHorizontalBgPaint.Color = Color.Beige;//mRoundCornerBackgroundColor;
            mHorizontalBgPaint.Dither = true;
            mHorizontalBgPaint.FilterBitmap = true;
            mHorizontalBgPaint.StrokeJoin = Join.Round;
            mHorizontalBgPaint.SetStyle(Paint.Style.Stroke);
            mHorizontalBgPaint.StrokeCap = Paint.Cap.Round;

            mProgressPaint = new Paint(PaintFlags.AntiAlias);
            mProgressPaint.StrokeWidth = mDefaultRoundConrnerWidth;
            mProgressPaint.Dither = true;
            mProgressPaint.FilterBitmap = true;
            mProgressPaint.StrokeJoin = Join.Round;
            mProgressPaint.Color = Color.Black; //mRoundCornerProgressColor;
            mProgressPaint.SetStyle(Paint.Style.Stroke);
            mProgressPaint.StrokeCap = Paint.Cap.Round;

            mPathViewHeight = (int)Math.Ceiling(mProgressPaint.StrokeWidth);
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            mHorizontalBgPath.MoveTo(mInitMoveToX, mInitMoveToY);
            mHorizontalBgPath.LineTo(mProgressBarWidth, mInitMoveToY);
            mProgressPath.MoveTo(mInitMoveToX, mInitMoveToY);
            float minX = (mProgressBarWidth * 1.0f * GetProgress()) / mMax * 1.0f;
            if (((int)Math.Floor(minX)) < mInitMoveToX)
            {
                minX = mInitMoveToX;
            }
            mProgressPath.LineTo(minX, mInitMoveToY);
            canvas.DrawPath(mHorizontalBgPath, mHorizontalBgPaint);
            canvas.DrawPath(mProgressPath, mProgressPaint);
        }

        public void SetProgress(long progress)
        {
            mProgress = progress;
            if (mProgress > mMax)
            {
                mProgress = mMax;
            }

            PostInvalidate();
        }

        public long GetProgress()
        {
            return mProgress;
        }

        public void SetMax(long max)
        {
            mMax = max;
        }

        public long GetMax()
        {
            return mMax;
        }

        int Dip2px(Context context, float dpValue)
        {
            float scale = context.Resources.DisplayMetrics.Density;
            return (int)(dpValue * scale + 0.5f);
        }
    }
}

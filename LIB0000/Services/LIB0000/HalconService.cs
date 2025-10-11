using HalconDotNet;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Media;
using Windows.Media.Ocr;

namespace LIB0000
{
    public partial class HalconService : ObservableObject
    {

        #region Commands

        [RelayCommand]

        private void cmdTeach()
        {
            GrabFromFile();
            if (!deepOcrInitialized)
            {    DeepOcrInit(); deepOcrInitialized = true; }
            DeepOcrDetect(Grab.Image);
            //action();
            //DetectShape();
            //GrabFromGigE();
            //DetectShape();
            //GrabFromUSB3();
            //TeachShape();
        }

        #endregion

        #region Constructor

        public HalconService()
        {
            Grab = new Grab();
            ShapeSearch = new ShapeSearch();


            Task.Run(() => Cyclic());


        }

        #endregion

        #region Events

        public event EventHandler<bool> OcrCompleted;

        protected virtual void OnOcrCompleted(bool success)
        {
            OcrCompleted?.Invoke(this, success);
        }

        #endregion

        #region Fields
        private HFramegrabber framegrabber;
        HTuple hv_PathExample = new HTuple(), hv_ImagesPath = new HTuple();
        HTuple hv_UseFastAI2Devices = new HTuple(), hv_DLDevice = new HTuple();
        HTuple hv_DeepOcrHandle = new HTuple(), hv_RecognitionAlphabet = new HTuple();
        HTuple hv_DisplaySize = new HTuple(), hv_WindowHandleAlphabet = new HTuple();
        HTuple hv_PreprocessedDisplayWidth = new HTuple(), hv_PreprocessedDisplayHeight = new HTuple();
        HTuple hv_ScoreMapsDisplayWidth = new HTuple(), hv_ScoreMapsDisplayHeight = new HTuple();
        HTuple hv_WindowHandle = new HTuple(), hv_WindowPreprocessed = new HTuple();
        HTuple hv_WindowScoreMaps = new HTuple(), hv_Index = new HTuple();
        HTuple hv_StartTime = new HTuple(), hv_Width = new HTuple();
        HTuple hv_Height = new HTuple(), hv_DeepOcrResult = new HTuple();
        HTuple hv_EndTime = new HTuple(), hv_TimeTaken = new HTuple();
        HTuple hv_WordBoxRow = new HTuple(), hv_WordBoxColumn = new HTuple();
        HTuple hv_WordBoxPhi = new HTuple(), hv_WordBoxLength1 = new HTuple();
        HTuple hv_WordBoxLength2 = new HTuple(), hv_Word = new HTuple();
        HTuple hv_ResultKey = new HTuple();
        HTuple hv_Words = new HTuple();
        int fileIndex = 0;

        bool deepOcrInitialized = false;
        bool busy = false;
        #endregion

        #region Methods

        public void dev_display_deep_ocr_results(HObject ho_Image, HTuple hv_WindowHandle,
    HTuple hv_DeepOcrResult, HTuple hv_GenParamName, HTuple hv_GenParamValue)
        {




            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_PreprocessedImage = null, ho_ScoreMapsImage = null;
            HObject ho_WordRectangle = null, ho_Arrow = null;

            // Local copy input parameter variables 
            HObject ho_Image_COPY_INP_TMP;
            ho_Image_COPY_INP_TMP = new HObject(ho_Image);



            // Local control variables 

            HTuple hv_NumberIconicObjects = new HTuple();
            HTuple hv_BoxColor = new HTuple(), hv_LineWidth = new HTuple();
            HTuple hv_FontSize = new HTuple(), hv_ShowWords = new HTuple();
            HTuple hv_ShowOrientation = new HTuple(), hv_ActiveWindow = new HTuple();
            HTuple hv_ActiveFont = new HTuple(), hv_Font = new HTuple();
            HTuple hv_DetectionMode = new HTuple(), hv_RecognitionMode = new HTuple();
            HTuple hv_Words = new HTuple(), hv_HasImageKey = new HTuple();
            HTuple hv_HasScoreMapsKey = new HTuple(), hv_Width = new HTuple();
            HTuple hv_Height = new HTuple(), hv_WordBoxesKey = new HTuple();
            HTuple hv_PreprocessedImageWidth = new HTuple(), hv_PreprocessedImageHeight = new HTuple();
            HTuple hv_ScoreMapsImageWidth = new HTuple(), hv_ScoreMapsImageHeight = new HTuple();
            HTuple hv_Row = new HTuple(), hv_Col = new HTuple(), hv_Phi = new HTuple();
            HTuple hv_Length1 = new HTuple(), hv_Length2 = new HTuple();
            HTuple hv_ArrowSizeFactorLength = new HTuple(), hv_ArrowSizeFactorHead = new HTuple();
            HTuple hv_MaxLengthArrow = new HTuple(), hv_HalfLengthArrow = new HTuple();
            HTuple hv_ArrowBaseRow = new HTuple(), hv_ArrowBaseCol = new HTuple();
            HTuple hv_ArrowHeadRow = new HTuple(), hv_ArrowHeadCol = new HTuple();
            HTuple hv_ArrowHeadSize = new HTuple(), hv_HasRecognition = new HTuple();
            HTuple hv_RecognizedWord = new HTuple(), hv_D = new HTuple();
            HTuple hv_Alpha = new HTuple(), hv_WordRow = new HTuple();
            HTuple hv_WordCol = new HTuple(), hv_Ascent = new HTuple();
            HTuple hv_Descent = new HTuple(), hv__ = new HTuple();
            HTuple hv_MarginBottom = new HTuple(), hv_WindowWidth = new HTuple();
            HTuple hv_WindowHeight = new HTuple(), hv_ImageWidth = new HTuple();
            HTuple hv_ImageHeight = new HTuple(), hv_ZoomedImageHeight = new HTuple();
            HTuple hv_ZoomedImageWidth = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_PreprocessedImage);
            HOperatorSet.GenEmptyObj(out ho_ScoreMapsImage);
            HOperatorSet.GenEmptyObj(out ho_WordRectangle);
            HOperatorSet.GenEmptyObj(out ho_Arrow);
            try
            {
                //This procedure visualizes the results DeepOcrResult of a Deep OCR model.
                //
                //
                hv_NumberIconicObjects.Dispose();
                HOperatorSet.CountObj(ho_Image_COPY_INP_TMP, out hv_NumberIconicObjects);
                if ((int)(new HTuple(hv_NumberIconicObjects.TupleGreater(1))) != 0)
                {
                    throw new HalconException("Only single input image allowed.");
                }
                //
                if ((int)(new HTuple((new HTuple(hv_DeepOcrResult.TupleLength())).TupleGreater(
                    1))) != 0)
                {
                    throw new HalconException("Only single result dictionary allowed.");
                }
                //
                //Parse generic visualization parameters.
                hv_BoxColor.Dispose(); hv_LineWidth.Dispose(); hv_FontSize.Dispose(); hv_ShowWords.Dispose(); hv_ShowOrientation.Dispose();
                parse_generic_visualization_parameters(hv_GenParamName, hv_GenParamValue, out hv_BoxColor,
                    out hv_LineWidth, out hv_FontSize, out hv_ShowWords, out hv_ShowOrientation);
                //
                //Prepare the window.
                HOperatorSet.SetWindowParam(hv_WindowHandle, "flush", "false");
                if (HDevWindowStack.IsOpen())
                {
                    hv_ActiveWindow = HDevWindowStack.GetActive();
                }
                if ((int)(new HTuple(hv_ActiveWindow.TupleNotEqual(hv_WindowHandle))) != 0)
                {
                    HDevWindowStack.SetActive(hv_WindowHandle);
                }
                hv_ActiveFont.Dispose();
                HOperatorSet.GetFont(hv_WindowHandle, out hv_ActiveFont);
                hv_Font.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_Font = "default-Normal-" + hv_FontSize;
                }
                if ((int)(new HTuple(hv_ActiveFont.TupleNotEqual(hv_Font))) != 0)
                {
                    HOperatorSet.SetFont(hv_WindowHandle, hv_Font);
                }
                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.ClearWindow(HDevWindowStack.GetActive());
                }
                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.SetLut(HDevWindowStack.GetActive(), "default");
                }
                //
                //Check whether detection and recognition results are available.
                hv_DetectionMode.Dispose();
                HOperatorSet.GetDictParam(hv_DeepOcrResult, "key_exists", "words", out hv_DetectionMode);
                hv_RecognitionMode.Dispose();
                HOperatorSet.GetDictParam(hv_DeepOcrResult, "key_exists", "word", out hv_RecognitionMode);
                //
                if ((int)(hv_DetectionMode) != 0)
                {
                    //Visualize the oriented rectangles marking the detected words.
                    //
                    if (HDevWindowStack.IsOpen())
                    {
                        HOperatorSet.DispObj(ho_Image_COPY_INP_TMP, HDevWindowStack.GetActive()
                            );
                    }
                    //
                    //Determine the dictionary key with word boxes to use based on the size of Image.
                    hv_Words.Dispose();
                    HOperatorSet.GetDictTuple(hv_DeepOcrResult, "words", out hv_Words);
                    hv_HasImageKey.Dispose();
                    HOperatorSet.GetDictParam(hv_DeepOcrResult, "key_exists", "image", out hv_HasImageKey);
                    hv_HasScoreMapsKey.Dispose();
                    HOperatorSet.GetDictParam(hv_DeepOcrResult, "key_exists", "score_maps", out hv_HasScoreMapsKey);
                    hv_Width.Dispose(); hv_Height.Dispose();
                    HOperatorSet.GetImageSize(ho_Image_COPY_INP_TMP, out hv_Width, out hv_Height);
                    hv_WordBoxesKey.Dispose();
                    hv_WordBoxesKey = "words";
                    if ((int)(hv_HasImageKey) != 0)
                    {
                        ho_PreprocessedImage.Dispose();
                        HOperatorSet.GetDictObject(out ho_PreprocessedImage, hv_DeepOcrResult,
                            "image");
                        hv_PreprocessedImageWidth.Dispose(); hv_PreprocessedImageHeight.Dispose();
                        HOperatorSet.GetImageSize(ho_PreprocessedImage, out hv_PreprocessedImageWidth,
                            out hv_PreprocessedImageHeight);
                        if ((int)((new HTuple(hv_Width.TupleEqual(hv_PreprocessedImageWidth))).TupleAnd(
                            new HTuple(hv_Height.TupleEqual(hv_PreprocessedImageHeight)))) != 0)
                        {
                            hv_WordBoxesKey.Dispose();
                            hv_WordBoxesKey = "word_boxes_on_image";
                        }
                    }
                    else if ((int)(hv_HasScoreMapsKey) != 0)
                    {
                        ho_ScoreMapsImage.Dispose();
                        HOperatorSet.GetDictObject(out ho_ScoreMapsImage, hv_DeepOcrResult, "score_maps");
                        hv_ScoreMapsImageWidth.Dispose(); hv_ScoreMapsImageHeight.Dispose();
                        HOperatorSet.GetImageSize(ho_ScoreMapsImage, out hv_ScoreMapsImageWidth,
                            out hv_ScoreMapsImageHeight);
                        if ((int)((new HTuple(hv_Width.TupleEqual(hv_ScoreMapsImageWidth))).TupleAnd(
                            new HTuple(hv_Height.TupleEqual(hv_ScoreMapsImageHeight)))) != 0)
                        {
                            hv_WordBoxesKey.Dispose();
                            hv_WordBoxesKey = "word_boxes_on_score_maps";
                        }
                    }
                    //
                    //Get rectangle2 boxes of detected words.
                    hv_Row.Dispose(); hv_Col.Dispose(); hv_Phi.Dispose(); hv_Length1.Dispose(); hv_Length2.Dispose();
                    get_deep_ocr_detection_word_boxes(hv_DeepOcrResult, hv_WordBoxesKey, out hv_Row,
                        out hv_Col, out hv_Phi, out hv_Length1, out hv_Length2);
                    //
                    if ((int)(new HTuple((new HTuple(hv_Row.TupleLength())).TupleGreater(0))) != 0)
                    {
                        //Generate XLD contours of the word rectangles.
                        ho_WordRectangle.Dispose();
                        HOperatorSet.GenRectangle2ContourXld(out ho_WordRectangle, hv_Row, hv_Col,
                            hv_Phi, hv_Length1, hv_Length2);
                        //
                        if ((int)(hv_ShowOrientation) != 0)
                        {
                            //Generate orientation arrows.
                            hv_ArrowSizeFactorLength.Dispose();
                            hv_ArrowSizeFactorLength = 0.4;
                            hv_ArrowSizeFactorHead.Dispose();
                            hv_ArrowSizeFactorHead = 0.2;
                            hv_MaxLengthArrow.Dispose();
                            hv_MaxLengthArrow = 20;
                            hv_HalfLengthArrow.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_HalfLengthArrow = hv_MaxLengthArrow.TupleMin2(
                                    hv_Length1 * hv_ArrowSizeFactorLength);
                            }
                            hv_ArrowBaseRow.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_ArrowBaseRow = hv_Row - ((hv_Length1 - hv_HalfLengthArrow) * (hv_Phi.TupleSin()
                                    ));
                            }
                            hv_ArrowBaseCol.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_ArrowBaseCol = hv_Col + ((hv_Length1 - hv_HalfLengthArrow) * (hv_Phi.TupleCos()
                                    ));
                            }
                            hv_ArrowHeadRow.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_ArrowHeadRow = hv_Row - ((hv_Length1 + hv_HalfLengthArrow) * (hv_Phi.TupleSin()
                                    ));
                            }
                            hv_ArrowHeadCol.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_ArrowHeadCol = hv_Col + ((hv_Length1 + hv_HalfLengthArrow) * (hv_Phi.TupleCos()
                                    ));
                            }
                            hv_ArrowHeadSize.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_ArrowHeadSize = (hv_MaxLengthArrow.TupleMin2(
                                    hv_Length1.TupleMin2(hv_Length2))) * hv_ArrowSizeFactorHead;
                            }
                            ho_Arrow.Dispose();
                            gen_arrow_contour_xld(out ho_Arrow, hv_ArrowBaseRow, hv_ArrowBaseCol,
                                hv_ArrowHeadRow, hv_ArrowHeadCol, hv_ArrowHeadSize, hv_ArrowHeadSize);
                        }
                        //
                        //Display black boundaries around rectangles and arrows.
                        if (HDevWindowStack.IsOpen())
                        {
                            HOperatorSet.SetColor(HDevWindowStack.GetActive(), "black");
                        }
                        if (HDevWindowStack.IsOpen())
                        {
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                HOperatorSet.SetLineWidth(HDevWindowStack.GetActive(), hv_LineWidth + 2);
                            }
                        }
                        if (HDevWindowStack.IsOpen())
                        {
                            HOperatorSet.DispObj(ho_WordRectangle, HDevWindowStack.GetActive());
                        }
                        if ((int)(hv_ShowOrientation) != 0)
                        {
                            if (HDevWindowStack.IsOpen())
                            {
                                HOperatorSet.DispObj(ho_Arrow, HDevWindowStack.GetActive());
                            }
                        }
                        //
                        //Display rectangles and arrows.
                        if (HDevWindowStack.IsOpen())
                        {
                            HOperatorSet.SetColor(HDevWindowStack.GetActive(), hv_BoxColor);
                        }
                        if (HDevWindowStack.IsOpen())
                        {
                            HOperatorSet.SetLineWidth(HDevWindowStack.GetActive(), hv_LineWidth);
                        }
                        if (HDevWindowStack.IsOpen())
                        {
                            HOperatorSet.DispObj(ho_WordRectangle, HDevWindowStack.GetActive());
                        }
                        if ((int)(hv_ShowOrientation) != 0)
                        {
                            if (HDevWindowStack.IsOpen())
                            {
                                HOperatorSet.DispObj(ho_Arrow, HDevWindowStack.GetActive());
                            }
                        }
                        //
                        //Display recognized words, if available.
                        if ((int)(hv_ShowWords) != 0)
                        {
                            //
                            //Check whether the result contains recognized words.
                            hv_HasRecognition.Dispose();
                            HOperatorSet.GetDictParam(hv_Words, "key_exists", "word", out hv_HasRecognition);
                            if ((int)(hv_HasRecognition) != 0)
                            {
                                hv_RecognizedWord.Dispose();
                                HOperatorSet.GetDictTuple(hv_Words, "word", out hv_RecognizedWord);
                                //
                                //Display each recognized word at the bottom-left corner of the respective word box.
                                if ((int)(new HTuple((new HTuple(hv_RecognizedWord.TupleLength())).TupleGreater(
                                    0))) != 0)
                                {
                                    hv_D.Dispose();
                                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                    {
                                        hv_D = hv_Length1.TupleHypot(
                                            hv_Length2);
                                    }
                                    hv_Alpha.Dispose();
                                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                    {
                                        hv_Alpha = hv_Length2.TupleAtan2(
                                            hv_Length1);
                                    }
                                    hv_WordRow.Dispose();
                                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                    {
                                        hv_WordRow = hv_Row + ((((hv_Alpha + hv_Phi)).TupleSin()
                                            ) * hv_D);
                                    }
                                    hv_WordCol.Dispose();
                                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                    {
                                        hv_WordCol = hv_Col - ((((hv_Alpha + hv_Phi)).TupleCos()
                                            ) * hv_D);
                                    }
                                    if (HDevWindowStack.IsOpen())
                                    {
                                        HOperatorSet.DispText(HDevWindowStack.GetActive(), hv_RecognizedWord,
                                            "image", hv_WordRow, hv_WordCol, "white", (new HTuple("box_color")).TupleConcat(
                                            "shadow"), (new HTuple("black")).TupleConcat("false"));
                                    }
                                }
                            }
                        }
                    }
                }
                else if ((int)(hv_RecognitionMode) != 0)
                {
                    //Recognition mode: Show the recognized word.
                    //
                    hv_RecognizedWord.Dispose();
                    HOperatorSet.GetDictTuple(hv_DeepOcrResult, "word", out hv_RecognizedWord);
                    if ((int)(new HTuple((new HTuple(hv_RecognizedWord.TupleLength())).TupleGreater(
                        1))) != 0)
                    {
                        throw new HalconException("In recognition mode only a single word can be processed.");
                    }
                    //
                    hv_Ascent.Dispose(); hv_Descent.Dispose(); hv__.Dispose(); hv__.Dispose();
                    HOperatorSet.GetStringExtents(hv_WindowHandle, hv_RecognizedWord, out hv_Ascent,
                        out hv_Descent, out hv__, out hv__);
                    hv_MarginBottom.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_MarginBottom = (hv_Ascent + hv_Descent) + 5;
                    }
                    //
                    //Zoom the image to fit into the window.
                    hv__.Dispose(); hv__.Dispose(); hv_WindowWidth.Dispose(); hv_WindowHeight.Dispose();
                    HOperatorSet.GetWindowExtents(hv_WindowHandle, out hv__, out hv__, out hv_WindowWidth,
                        out hv_WindowHeight);
                    hv_ImageWidth.Dispose(); hv_ImageHeight.Dispose();
                    HOperatorSet.GetImageSize(ho_Image_COPY_INP_TMP, out hv_ImageWidth, out hv_ImageHeight);
                    hv_ZoomedImageHeight.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_ZoomedImageHeight = (((new HTuple(0)).TupleMax2(
                            hv_WindowHeight - hv_MarginBottom))).TupleInt();
                    }
                    if ((int)(new HTuple(hv_ZoomedImageHeight.TupleEqual(0))) != 0)
                    {
                        throw new HalconException("Font size too large for given window size.");
                    }
                    hv_ZoomedImageWidth.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_ZoomedImageWidth = (((hv_ImageWidth / (hv_ImageHeight.TupleReal()
                            )) * hv_ZoomedImageHeight)).TupleInt();
                    }
                    if ((int)(new HTuple(hv_ZoomedImageWidth.TupleGreater(hv_WindowWidth))) != 0)
                    {
                        hv_ZoomedImageWidth.Dispose();
                        hv_ZoomedImageWidth = new HTuple(hv_WindowWidth);
                        hv_ZoomedImageHeight.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_ZoomedImageHeight = (((hv_ImageHeight / (hv_ImageWidth.TupleReal()
                                )) * hv_ZoomedImageWidth)).TupleInt();
                        }
                    }
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.ZoomImageSize(ho_Image_COPY_INP_TMP, out ExpTmpOutVar_0, hv_ZoomedImageWidth,
                            hv_ZoomedImageHeight, "constant");
                        ho_Image_COPY_INP_TMP.Dispose();
                        ho_Image_COPY_INP_TMP = ExpTmpOutVar_0;
                    }
                    //
                    //Display the image.
                    if (HDevWindowStack.IsOpen())
                    {
                        HOperatorSet.SetPart(HDevWindowStack.GetActive(), 0, 0, hv_WindowHeight,
                            hv_WindowWidth);
                    }
                    if (HDevWindowStack.IsOpen())
                    {
                        HOperatorSet.DispObj(ho_Image_COPY_INP_TMP, HDevWindowStack.GetActive()
                            );
                    }
                    //
                    //Display the recognized word below the image.
                    if ((int)(hv_ShowWords) != 0)
                    {
                        if (HDevWindowStack.IsOpen())
                        {
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                HOperatorSet.DispText(HDevWindowStack.GetActive(), hv_RecognizedWord,
                                    "image", hv_WindowHeight - hv_MarginBottom, 5, "white", "box", "false");
                            }
                        }
                    }
                }
                else
                {
                    //Only display the image.
                    if (HDevWindowStack.IsOpen())
                    {
                        HOperatorSet.DispObj(ho_Image_COPY_INP_TMP, HDevWindowStack.GetActive()
                            );
                    }
                }
                //
                //Update window.
                HOperatorSet.SetWindowParam(hv_WindowHandle, "flush", "true");
                HOperatorSet.FlushBuffer(hv_WindowHandle);
                //
                //Restore the original values.
                if ((int)(new HTuple(hv_ActiveWindow.TupleNotEqual(hv_WindowHandle))) != 0)
                {
                    HDevWindowStack.SetActive(hv_ActiveWindow);
                }
                if ((int)(new HTuple(hv_ActiveFont.TupleNotEqual(hv_Font))) != 0)
                {
                    HOperatorSet.SetFont(hv_WindowHandle, hv_ActiveFont);
                }
                //
                ho_Image_COPY_INP_TMP.Dispose();
                ho_PreprocessedImage.Dispose();
                ho_ScoreMapsImage.Dispose();
                ho_WordRectangle.Dispose();
                ho_Arrow.Dispose();

                hv_NumberIconicObjects.Dispose();
                hv_BoxColor.Dispose();
                hv_LineWidth.Dispose();
                hv_FontSize.Dispose();
                hv_ShowWords.Dispose();
                hv_ShowOrientation.Dispose();
                hv_ActiveWindow.Dispose();
                hv_ActiveFont.Dispose();
                hv_Font.Dispose();
                hv_DetectionMode.Dispose();
                hv_RecognitionMode.Dispose();
                hv_Words.Dispose();
                hv_HasImageKey.Dispose();
                hv_HasScoreMapsKey.Dispose();
                hv_Width.Dispose();
                hv_Height.Dispose();
                hv_WordBoxesKey.Dispose();
                hv_PreprocessedImageWidth.Dispose();
                hv_PreprocessedImageHeight.Dispose();
                hv_ScoreMapsImageWidth.Dispose();
                hv_ScoreMapsImageHeight.Dispose();
                hv_Row.Dispose();
                hv_Col.Dispose();
                hv_Phi.Dispose();
                hv_Length1.Dispose();
                hv_Length2.Dispose();
                hv_ArrowSizeFactorLength.Dispose();
                hv_ArrowSizeFactorHead.Dispose();
                hv_MaxLengthArrow.Dispose();
                hv_HalfLengthArrow.Dispose();
                hv_ArrowBaseRow.Dispose();
                hv_ArrowBaseCol.Dispose();
                hv_ArrowHeadRow.Dispose();
                hv_ArrowHeadCol.Dispose();
                hv_ArrowHeadSize.Dispose();
                hv_HasRecognition.Dispose();
                hv_RecognizedWord.Dispose();
                hv_D.Dispose();
                hv_Alpha.Dispose();
                hv_WordRow.Dispose();
                hv_WordCol.Dispose();
                hv_Ascent.Dispose();
                hv_Descent.Dispose();
                hv__.Dispose();
                hv_MarginBottom.Dispose();
                hv_WindowWidth.Dispose();
                hv_WindowHeight.Dispose();
                hv_ImageWidth.Dispose();
                hv_ImageHeight.Dispose();
                hv_ZoomedImageHeight.Dispose();
                hv_ZoomedImageWidth.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {
                ho_Image_COPY_INP_TMP.Dispose();
                ho_PreprocessedImage.Dispose();
                ho_ScoreMapsImage.Dispose();
                ho_WordRectangle.Dispose();
                ho_Arrow.Dispose();

                hv_NumberIconicObjects.Dispose();
                hv_BoxColor.Dispose();
                hv_LineWidth.Dispose();
                hv_FontSize.Dispose();
                hv_ShowWords.Dispose();
                hv_ShowOrientation.Dispose();
                hv_ActiveWindow.Dispose();
                hv_ActiveFont.Dispose();
                hv_Font.Dispose();
                hv_DetectionMode.Dispose();
                hv_RecognitionMode.Dispose();
                hv_Words.Dispose();
                hv_HasImageKey.Dispose();
                hv_HasScoreMapsKey.Dispose();
                hv_Width.Dispose();
                hv_Height.Dispose();
                hv_WordBoxesKey.Dispose();
                hv_PreprocessedImageWidth.Dispose();
                hv_PreprocessedImageHeight.Dispose();
                hv_ScoreMapsImageWidth.Dispose();
                hv_ScoreMapsImageHeight.Dispose();
                hv_Row.Dispose();
                hv_Col.Dispose();
                hv_Phi.Dispose();
                hv_Length1.Dispose();
                hv_Length2.Dispose();
                hv_ArrowSizeFactorLength.Dispose();
                hv_ArrowSizeFactorHead.Dispose();
                hv_MaxLengthArrow.Dispose();
                hv_HalfLengthArrow.Dispose();
                hv_ArrowBaseRow.Dispose();
                hv_ArrowBaseCol.Dispose();
                hv_ArrowHeadRow.Dispose();
                hv_ArrowHeadCol.Dispose();
                hv_ArrowHeadSize.Dispose();
                hv_HasRecognition.Dispose();
                hv_RecognizedWord.Dispose();
                hv_D.Dispose();
                hv_Alpha.Dispose();
                hv_WordRow.Dispose();
                hv_WordCol.Dispose();
                hv_Ascent.Dispose();
                hv_Descent.Dispose();
                hv__.Dispose();
                hv_MarginBottom.Dispose();
                hv_WindowWidth.Dispose();
                hv_WindowHeight.Dispose();
                hv_ImageWidth.Dispose();
                hv_ImageHeight.Dispose();
                hv_ZoomedImageHeight.Dispose();
                hv_ZoomedImageWidth.Dispose();

                throw HDevExpDefaultException;
            }
        }

        // Chapter: OCR / Deep OCR
        // Short Description: Visualize Deep OCR detection and recognition results. 
        public void dev_display_deep_ocr_results_preprocessed(HTuple hv_WindowHandle,
            HTuple hv_DeepOcrResult, HTuple hv_GenParamName, HTuple hv_GenParamValue)
        {



            // Local iconic variables 

            HObject ho_PreprocessedImage;

            // Local control variables 

            HTuple hv_DetectionMode = new HTuple(), hv_HasImageKey = new HTuple();
            HTuple hv_ActiveWindow = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_PreprocessedImage);
            try
            {
                //This procedure visualizes the results DeepOcrResult of a Deep OCR model
                //on the preprocessed image contained in DeepOcrResult.
                //
                //
                if ((int)(new HTuple((new HTuple(hv_DeepOcrResult.TupleLength())).TupleGreater(
                    1))) != 0)
                {
                    throw new HalconException("Only single result dictionary allowed.");
                }
                //
                //Validate key for detection mode.
                hv_DetectionMode.Dispose();
                HOperatorSet.GetDictParam(hv_DeepOcrResult, "key_exists", "words", out hv_DetectionMode);
                if ((int)(hv_DetectionMode.TupleNot()) != 0)
                {
                    throw new HalconException("Only results for detection or auto mode are allowed.");
                }
                //
                //Extract the preprocessed image stored in the key 'image'.
                hv_HasImageKey.Dispose();
                HOperatorSet.GetDictParam(hv_DeepOcrResult, "key_exists", "image", out hv_HasImageKey);
                if ((int)(hv_HasImageKey.TupleNot()) != 0)
                {
                    throw new HalconException("Preprocessed image not available in dictionary.");
                }
                ho_PreprocessedImage.Dispose();
                HOperatorSet.GetDictObject(out ho_PreprocessedImage, hv_DeepOcrResult, "image");
                //
                //Visualize the results on the preprocessed image.
                dev_display_deep_ocr_results(ho_PreprocessedImage, hv_WindowHandle, hv_DeepOcrResult,
                    hv_GenParamName, hv_GenParamValue);
                //
                //Display the window title.
                if (HDevWindowStack.IsOpen())
                {
                    hv_ActiveWindow = HDevWindowStack.GetActive();
                }
                if ((int)(new HTuple(hv_ActiveWindow.TupleNotEqual(hv_WindowHandle))) != 0)
                {
                    HDevWindowStack.SetActive(hv_WindowHandle);
                }
                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.DispText(HDevWindowStack.GetActive(), "Preprocessed image",
                        "image", 10, 10, "black", new HTuple(), new HTuple());
                }
                HDevWindowStack.SetActive(hv_ActiveWindow);
                //
                ho_PreprocessedImage.Dispose();

                hv_DetectionMode.Dispose();
                hv_HasImageKey.Dispose();
                hv_ActiveWindow.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {
                ho_PreprocessedImage.Dispose();

                hv_DetectionMode.Dispose();
                hv_HasImageKey.Dispose();
                hv_ActiveWindow.Dispose();

                throw HDevExpDefaultException;
            }
        }

        // Chapter: OCR / Deep OCR
        // Short Description: Display Deep OCR score maps. 
        public void dev_display_deep_ocr_score_maps(HTuple hv_WindowHandle, HTuple hv_DeepOcrResult,
            HTuple hv_GenParamName, HTuple hv_GenParamValue)
        {



            // Local iconic variables 

            HObject ho_ScoreMaps, ho_CharacterScore, ho_LinkScore;
            HObject ho_ScoreMapsConcat, ho_ScoreMapsTiled, ho_Line;

            // Local control variables 

            HTuple hv_ActiveWindow = new HTuple(), hv_Width = new HTuple();
            HTuple hv_Height = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_ScoreMaps);
            HOperatorSet.GenEmptyObj(out ho_CharacterScore);
            HOperatorSet.GenEmptyObj(out ho_LinkScore);
            HOperatorSet.GenEmptyObj(out ho_ScoreMapsConcat);
            HOperatorSet.GenEmptyObj(out ho_ScoreMapsTiled);
            HOperatorSet.GenEmptyObj(out ho_Line);
            try
            {
                //This procedure visualizes the character and link scores
                //returned by the detection component of the Deep OCR model.
                //
                if ((int)(new HTuple((new HTuple(hv_DeepOcrResult.TupleLength())).TupleGreater(
                    1))) != 0)
                {
                    throw new HalconException("Only single result dictionary allowed.");
                }
                //
                if (HDevWindowStack.IsOpen())
                {
                    hv_ActiveWindow = HDevWindowStack.GetActive();
                }
                if ((int)(new HTuple(hv_ActiveWindow.TupleNotEqual(hv_WindowHandle))) != 0)
                {
                    HDevWindowStack.SetActive(hv_WindowHandle);
                }
                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.ClearWindow(HDevWindowStack.GetActive());
                }
                HOperatorSet.SetWindowParam(hv_WindowHandle, "flush", "false");
                //
                //Extract score maps from the result dictionary.
                ho_ScoreMaps.Dispose();
                HOperatorSet.GetDictObject(out ho_ScoreMaps, hv_DeepOcrResult, "score_maps");
                ho_CharacterScore.Dispose();
                HOperatorSet.AccessChannel(ho_ScoreMaps, out ho_CharacterScore, 1);
                ho_LinkScore.Dispose();
                HOperatorSet.AccessChannel(ho_ScoreMaps, out ho_LinkScore, 2);
                ho_ScoreMapsConcat.Dispose();
                HOperatorSet.ConcatObj(ho_CharacterScore, ho_LinkScore, out ho_ScoreMapsConcat
                    );
                ho_ScoreMapsTiled.Dispose();
                HOperatorSet.TileImages(ho_ScoreMapsConcat, out ho_ScoreMapsTiled, 2, "horizontal");
                hv_Width.Dispose(); hv_Height.Dispose();
                HOperatorSet.GetImageSize(ho_ScoreMaps, out hv_Width, out hv_Height);
                //
                //Display the score maps using the 'jet' color map.
                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.SetLut(HDevWindowStack.GetActive(), ((new HTuple("jet")).TupleConcat(
                        0)).TupleConcat(1));
                }
                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.DispObj(ho_ScoreMapsTiled, HDevWindowStack.GetActive());
                }
                if (HDevWindowStack.IsOpen())
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        HOperatorSet.DispText(HDevWindowStack.GetActive(), (new HTuple("Character score")).TupleConcat(
                            "Link score"), "image", (new HTuple(10)).TupleConcat(10), (new HTuple(10)).TupleConcat(
                            hv_Width + 10), "black", new HTuple(), new HTuple());
                    }
                }
                //
                //Draw a separating line between score maps.
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    ho_Line.Dispose();
                    HOperatorSet.GenContourPolygonXld(out ho_Line, (new HTuple(-0.5)).TupleConcat(
                        hv_Height - 0.5), ((hv_Width - 0.5)).TupleConcat(hv_Width - 0.5));
                }
                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.SetColor(HDevWindowStack.GetActive(), "white");
                }
                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.SetLineWidth(HDevWindowStack.GetActive(), 2);
                }
                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.DispObj(ho_Line, HDevWindowStack.GetActive());
                }
                //
                HOperatorSet.SetWindowParam(hv_WindowHandle, "flush", "true");
                HOperatorSet.FlushBuffer(hv_WindowHandle);
                //
                if ((int)(new HTuple(hv_ActiveWindow.TupleNotEqual(hv_WindowHandle))) != 0)
                {
                    HDevWindowStack.SetActive(hv_ActiveWindow);
                }
                //
                ho_ScoreMaps.Dispose();
                ho_CharacterScore.Dispose();
                ho_LinkScore.Dispose();
                ho_ScoreMapsConcat.Dispose();
                ho_ScoreMapsTiled.Dispose();
                ho_Line.Dispose();

                hv_ActiveWindow.Dispose();
                hv_Width.Dispose();
                hv_Height.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {
                ho_ScoreMaps.Dispose();
                ho_CharacterScore.Dispose();
                ho_LinkScore.Dispose();
                ho_ScoreMapsConcat.Dispose();
                ho_ScoreMapsTiled.Dispose();
                ho_Line.Dispose();

                hv_ActiveWindow.Dispose();
                hv_Width.Dispose();
                hv_Height.Dispose();

                throw HDevExpDefaultException;
            }
        }

        // Chapter: Develop
        // Short Description: Switch dev_update_pc, dev_update_var, and dev_update_window to 'off'. 
        public void dev_update_off()
        {

            // Initialize local and output iconic variables 
            //This procedure sets different update settings to 'off'.
            //This is useful to get the best performance and reduce overhead.
            //
            // dev_update_pc(...); only in hdevelop
            // dev_update_var(...); only in hdevelop
            // dev_update_window(...); only in hdevelop


            return;
        }

        // Chapter: XLD / Creation
        // Short Description: Create an arrow shaped XLD contour. 
        public void gen_arrow_contour_xld(out HObject ho_Arrow, HTuple hv_Row1, HTuple hv_Column1,
            HTuple hv_Row2, HTuple hv_Column2, HTuple hv_HeadLength, HTuple hv_HeadWidth)
        {



            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_TempArrow = null;

            // Local control variables 

            HTuple hv_Length = new HTuple(), hv_ZeroLengthIndices = new HTuple();
            HTuple hv_DR = new HTuple(), hv_DC = new HTuple(), hv_HalfHeadWidth = new HTuple();
            HTuple hv_RowP1 = new HTuple(), hv_ColP1 = new HTuple();
            HTuple hv_RowP2 = new HTuple(), hv_ColP2 = new HTuple();
            HTuple hv_Index = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Arrow);
            HOperatorSet.GenEmptyObj(out ho_TempArrow);
            try
            {
                //This procedure generates arrow shaped XLD contours,
                //pointing from (Row1, Column1) to (Row2, Column2).
                //If starting and end point are identical, a contour consisting
                //of a single point is returned.
                //
                //input parameters:
                //Row1, Column1: Coordinates of the arrows' starting points
                //Row2, Column2: Coordinates of the arrows' end points
                //HeadLength, HeadWidth: Size of the arrow heads in pixels
                //
                //output parameter:
                //Arrow: The resulting XLD contour
                //
                //The input tuples Row1, Column1, Row2, and Column2 have to be of
                //the same length.
                //HeadLength and HeadWidth either have to be of the same length as
                //Row1, Column1, Row2, and Column2 or have to be a single element.
                //If one of the above restrictions is violated, an error will occur.
                //
                //
                //Initialization.
                ho_Arrow.Dispose();
                HOperatorSet.GenEmptyObj(out ho_Arrow);
                //
                //Calculate the arrow length
                hv_Length.Dispose();
                HOperatorSet.DistancePp(hv_Row1, hv_Column1, hv_Row2, hv_Column2, out hv_Length);
                //
                //Mark arrows with identical start and end point
                //(set Length to -1 to avoid division-by-zero exception)
                hv_ZeroLengthIndices.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_ZeroLengthIndices = hv_Length.TupleFind(
                        0);
                }
                if ((int)(new HTuple(hv_ZeroLengthIndices.TupleNotEqual(-1))) != 0)
                {
                    if (hv_Length == null)
                        hv_Length = new HTuple();
                    hv_Length[hv_ZeroLengthIndices] = -1;
                }
                //
                //Calculate auxiliary variables.
                hv_DR.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_DR = (1.0 * (hv_Row2 - hv_Row1)) / hv_Length;
                }
                hv_DC.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_DC = (1.0 * (hv_Column2 - hv_Column1)) / hv_Length;
                }
                hv_HalfHeadWidth.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_HalfHeadWidth = hv_HeadWidth / 2.0;
                }
                //
                //Calculate end points of the arrow head.
                hv_RowP1.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_RowP1 = (hv_Row1 + ((hv_Length - hv_HeadLength) * hv_DR)) + (hv_HalfHeadWidth * hv_DC);
                }
                hv_ColP1.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_ColP1 = (hv_Column1 + ((hv_Length - hv_HeadLength) * hv_DC)) - (hv_HalfHeadWidth * hv_DR);
                }
                hv_RowP2.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_RowP2 = (hv_Row1 + ((hv_Length - hv_HeadLength) * hv_DR)) - (hv_HalfHeadWidth * hv_DC);
                }
                hv_ColP2.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_ColP2 = (hv_Column1 + ((hv_Length - hv_HeadLength) * hv_DC)) + (hv_HalfHeadWidth * hv_DR);
                }
                //
                //Finally create output XLD contour for each input point pair
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_Length.TupleLength())) - 1); hv_Index = (int)hv_Index + 1)
                {
                    if ((int)(new HTuple(((hv_Length.TupleSelect(hv_Index))).TupleEqual(-1))) != 0)
                    {
                        //Create_ single points for arrows with identical start and end point
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            ho_TempArrow.Dispose();
                            HOperatorSet.GenContourPolygonXld(out ho_TempArrow, hv_Row1.TupleSelect(
                                hv_Index), hv_Column1.TupleSelect(hv_Index));
                        }
                    }
                    else
                    {
                        //Create arrow contour
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            ho_TempArrow.Dispose();
                            HOperatorSet.GenContourPolygonXld(out ho_TempArrow, ((((((((((hv_Row1.TupleSelect(
                                hv_Index))).TupleConcat(hv_Row2.TupleSelect(hv_Index)))).TupleConcat(
                                hv_RowP1.TupleSelect(hv_Index)))).TupleConcat(hv_Row2.TupleSelect(hv_Index)))).TupleConcat(
                                hv_RowP2.TupleSelect(hv_Index)))).TupleConcat(hv_Row2.TupleSelect(hv_Index)),
                                ((((((((((hv_Column1.TupleSelect(hv_Index))).TupleConcat(hv_Column2.TupleSelect(
                                hv_Index)))).TupleConcat(hv_ColP1.TupleSelect(hv_Index)))).TupleConcat(
                                hv_Column2.TupleSelect(hv_Index)))).TupleConcat(hv_ColP2.TupleSelect(
                                hv_Index)))).TupleConcat(hv_Column2.TupleSelect(hv_Index)));
                        }
                    }
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.ConcatObj(ho_Arrow, ho_TempArrow, out ExpTmpOutVar_0);
                        ho_Arrow.Dispose();
                        ho_Arrow = ExpTmpOutVar_0;
                    }
                }
                ho_TempArrow.Dispose();

                hv_Length.Dispose();
                hv_ZeroLengthIndices.Dispose();
                hv_DR.Dispose();
                hv_DC.Dispose();
                hv_HalfHeadWidth.Dispose();
                hv_RowP1.Dispose();
                hv_ColP1.Dispose();
                hv_RowP2.Dispose();
                hv_ColP2.Dispose();
                hv_Index.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {
                ho_TempArrow.Dispose();

                hv_Length.Dispose();
                hv_ZeroLengthIndices.Dispose();
                hv_DR.Dispose();
                hv_DC.Dispose();
                hv_HalfHeadWidth.Dispose();
                hv_RowP1.Dispose();
                hv_ColP1.Dispose();
                hv_RowP2.Dispose();
                hv_ColP2.Dispose();
                hv_Index.Dispose();

                throw HDevExpDefaultException;
            }
        }

        // Chapter: OCR / Deep OCR
        // Short Description: Get the detection word boxes contained in the specified ResultKey. 
        private void get_deep_ocr_detection_word_boxes(HTuple hv_DeepOcrResult, HTuple hv_ResultKey,
            out HTuple hv_WordBoxRow, out HTuple hv_WordBoxCol, out HTuple hv_WordBoxPhi,
            out HTuple hv_WordBoxLength1, out HTuple hv_WordBoxLength2)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_SupportedKey = new HTuple(), hv_WordBoxDict = new HTuple();
            HTuple hv_SameSize = new HTuple();
            // Initialize local and output iconic variables 
            hv_WordBoxRow = new HTuple();
            hv_WordBoxCol = new HTuple();
            hv_WordBoxPhi = new HTuple();
            hv_WordBoxLength1 = new HTuple();
            hv_WordBoxLength2 = new HTuple();

            try
            {
                //This procedure retrieves the rectangle2 parameters of the detection word boxes
                //contained in the specified result dictionary key.
                //
                hv_SupportedKey.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_SupportedKey = (new HTuple((new HTuple(hv_ResultKey.TupleEqual(
                        "words"))).TupleOr(new HTuple(hv_ResultKey.TupleEqual("word_boxes_on_image"))))).TupleOr(
                        new HTuple(hv_ResultKey.TupleEqual("word_boxes_on_score_maps")));
                }
                if ((int)(hv_SupportedKey.TupleNot()) != 0)
                {
                    throw new HalconException("Unsupported dictionary key.");
                }
                //
                //Get detection rectangle2 parameters.
                hv_WordBoxDict.Dispose();
                HOperatorSet.GetDictTuple(hv_DeepOcrResult, hv_ResultKey, out hv_WordBoxDict);
                hv_WordBoxRow.Dispose();
                HOperatorSet.GetDictTuple(hv_WordBoxDict, "row", out hv_WordBoxRow);
                hv_WordBoxCol.Dispose();
                HOperatorSet.GetDictTuple(hv_WordBoxDict, "col", out hv_WordBoxCol);
                hv_WordBoxPhi.Dispose();
                HOperatorSet.GetDictTuple(hv_WordBoxDict, "phi", out hv_WordBoxPhi);
                hv_WordBoxLength1.Dispose();
                HOperatorSet.GetDictTuple(hv_WordBoxDict, "length1", out hv_WordBoxLength1);
                hv_WordBoxLength2.Dispose();
                HOperatorSet.GetDictTuple(hv_WordBoxDict, "length2", out hv_WordBoxLength2);
                hv_SameSize.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_SameSize = (new HTuple((new HTuple((new HTuple((new HTuple(hv_WordBoxRow.TupleLength()
                        )).TupleEqual(new HTuple(hv_WordBoxCol.TupleLength())))).TupleAnd(new HTuple((new HTuple(hv_WordBoxRow.TupleLength()
                        )).TupleEqual(new HTuple(hv_WordBoxPhi.TupleLength())))))).TupleAnd(new HTuple((new HTuple(hv_WordBoxRow.TupleLength()
                        )).TupleEqual(new HTuple(hv_WordBoxLength1.TupleLength())))))).TupleAnd(
                        new HTuple((new HTuple(hv_WordBoxRow.TupleLength())).TupleEqual(new HTuple(hv_WordBoxLength2.TupleLength()))));
                }
                if ((int)(hv_SameSize.TupleNot()) != 0)
                {
                    throw new HalconException("Rectangle2 parameters do not have the same size.");
                }
                //

                hv_SupportedKey.Dispose();
                hv_WordBoxDict.Dispose();
                hv_SameSize.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {

                hv_SupportedKey.Dispose();
                hv_WordBoxDict.Dispose();
                hv_SameSize.Dispose();

                throw HDevExpDefaultException;
            }
        }

        // Chapter: OCR / Deep OCR
        // Short Description: Parse generic visualization parameters. 
        private void parse_generic_visualization_parameters(HTuple hv_GenParamName, HTuple hv_GenParamValue,
            out HTuple hv_BoxColor, out HTuple hv_LineWidth, out HTuple hv_FontSize, out HTuple hv_ShowWords,
            out HTuple hv_ShowOrientation)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_ShowScoreMaps = new HTuple(), hv_ParamIdx = new HTuple();
            HTuple hv_AllowedGenParams = new HTuple(), hv_CaseIdx = new HTuple();
            HTuple hv_BoolShowWords = new HTuple(), hv_BoolShowArrow = new HTuple();
            // Initialize local and output iconic variables 
            hv_BoxColor = new HTuple();
            hv_LineWidth = new HTuple();
            hv_FontSize = new HTuple();
            hv_ShowWords = new HTuple();
            hv_ShowOrientation = new HTuple();
            try
            {
                //Set default values.
                hv_BoxColor.Dispose();
                hv_BoxColor = "green";
                hv_LineWidth.Dispose();
                hv_LineWidth = 3;
                hv_FontSize.Dispose();
                hv_FontSize = 12;
                hv_ShowScoreMaps.Dispose();
                hv_ShowScoreMaps = 1;
                hv_ShowWords.Dispose();
                hv_ShowWords = 1;
                hv_ShowOrientation.Dispose();
                hv_ShowOrientation = 1;

                //Parse the generic parameters.
                for (hv_ParamIdx = 0; (int)hv_ParamIdx <= (int)((new HTuple(hv_GenParamName.TupleLength()
                    )) - 1); hv_ParamIdx = (int)hv_ParamIdx + 1)
                {
                    hv_AllowedGenParams.Dispose();
                    hv_AllowedGenParams = new HTuple();
                    hv_AllowedGenParams[0] = "box_color";
                    hv_AllowedGenParams[1] = "line_width";
                    hv_AllowedGenParams[2] = "font_size";
                    hv_AllowedGenParams[3] = "show_words";
                    hv_AllowedGenParams[4] = "show_orientation";
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_CaseIdx.Dispose();
                        HOperatorSet.TupleFind(hv_AllowedGenParams, hv_GenParamName.TupleSelect(hv_ParamIdx),
                            out hv_CaseIdx);
                    }
                    switch (hv_CaseIdx.I)
                    {
                        case 0:
                            //Get color.
                            hv_BoxColor.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_BoxColor = hv_GenParamValue.TupleSelect(
                                    hv_ParamIdx);
                            }
                            break;
                        case 1:
                            //Get line width.
                            hv_LineWidth.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_LineWidth = hv_GenParamValue.TupleSelect(
                                    hv_ParamIdx);
                            }
                            break;
                        case 2:
                            //Get font size.
                            hv_FontSize.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_FontSize = hv_GenParamValue.TupleSelect(
                                    hv_ParamIdx);
                            }
                            break;
                        case 3:
                            //Check whether words shall be displayed.
                            hv_BoolShowWords.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_BoolShowWords = hv_GenParamValue.TupleSelect(
                                    hv_ParamIdx);
                            }
                            if ((int)(new HTuple(hv_BoolShowWords.TupleEqual("true"))) != 0)
                            {
                                hv_ShowWords.Dispose();
                                hv_ShowWords = 1;
                            }
                            else
                            {
                                hv_ShowWords.Dispose();
                                hv_ShowWords = 0;
                            }
                            break;
                        case 4:
                            //Check whether arrow of the word box should be displayed.
                            hv_BoolShowArrow.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_BoolShowArrow = hv_GenParamValue.TupleSelect(
                                    hv_ParamIdx);
                            }
                            if ((int)(new HTuple(hv_BoolShowArrow.TupleEqual("true"))) != 0)
                            {
                                hv_ShowOrientation.Dispose();
                                hv_ShowOrientation = 1;
                            }
                            else
                            {
                                hv_ShowOrientation.Dispose();
                                hv_ShowOrientation = 0;
                            }
                            break;
                        case -1:
                            //General parameter not valid.
                            throw new HalconException(("The general parameter \"" + (hv_GenParamName.TupleSelect(
                                hv_ParamIdx))) + "\" is not valid.");
                            break;
                    }
                }

                hv_ShowScoreMaps.Dispose();
                hv_ParamIdx.Dispose();
                hv_AllowedGenParams.Dispose();
                hv_CaseIdx.Dispose();
                hv_BoolShowWords.Dispose();
                hv_BoolShowArrow.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {

                hv_ShowScoreMaps.Dispose();
                hv_ParamIdx.Dispose();
                hv_AllowedGenParams.Dispose();
                hv_CaseIdx.Dispose();
                hv_BoolShowWords.Dispose();
                hv_BoolShowArrow.Dispose();

                throw HDevExpDefaultException;
            }
        }

        // Chapter: Graphics / Text
        // Short Description: Set font independent of OS 
        public void set_display_font(HTuple hv_WindowHandle, HTuple hv_Size, HTuple hv_Font,
            HTuple hv_Bold, HTuple hv_Slant)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_OS = new HTuple(), hv_Fonts = new HTuple();
            HTuple hv_Style = new HTuple(), hv_Exception = new HTuple();
            HTuple hv_AvailableFonts = new HTuple(), hv_Fdx = new HTuple();
            HTuple hv_Indices = new HTuple();
            HTuple hv_Font_COPY_INP_TMP = new HTuple(hv_Font);
            HTuple hv_Size_COPY_INP_TMP = new HTuple(hv_Size);

            // Initialize local and output iconic variables 
            try
            {
                //This procedure sets the text font of the current window with
                //the specified attributes.
                //
                //Input parameters:
                //WindowHandle: The graphics window for which the font will be set
                //Size: The font size. If Size=-1, the default of 16 is used.
                //Bold: If set to 'true', a bold font is used
                //Slant: If set to 'true', a slanted font is used
                //
                hv_OS.Dispose();
                HOperatorSet.GetSystem("operating_system", out hv_OS);
                if ((int)((new HTuple(hv_Size_COPY_INP_TMP.TupleEqual(new HTuple()))).TupleOr(
                    new HTuple(hv_Size_COPY_INP_TMP.TupleEqual(-1)))) != 0)
                {
                    hv_Size_COPY_INP_TMP.Dispose();
                    hv_Size_COPY_INP_TMP = 16;
                }
                if ((int)(new HTuple(((hv_OS.TupleSubstr(0, 2))).TupleEqual("Win"))) != 0)
                {
                    //Restore previous behavior
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_Size = ((1.13677 * hv_Size_COPY_INP_TMP)).TupleInt()
                                ;
                            hv_Size_COPY_INP_TMP.Dispose();
                            hv_Size_COPY_INP_TMP = ExpTmpLocalVar_Size;
                        }
                    }
                }
                else
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_Size = hv_Size_COPY_INP_TMP.TupleInt()
                                ;
                            hv_Size_COPY_INP_TMP.Dispose();
                            hv_Size_COPY_INP_TMP = ExpTmpLocalVar_Size;
                        }
                    }
                }
                if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("Courier"))) != 0)
                {
                    hv_Fonts.Dispose();
                    hv_Fonts = new HTuple();
                    hv_Fonts[0] = "Courier";
                    hv_Fonts[1] = "Courier 10 Pitch";
                    hv_Fonts[2] = "Courier New";
                    hv_Fonts[3] = "CourierNew";
                    hv_Fonts[4] = "Liberation Mono";
                }
                else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("mono"))) != 0)
                {
                    hv_Fonts.Dispose();
                    hv_Fonts = new HTuple();
                    hv_Fonts[0] = "Consolas";
                    hv_Fonts[1] = "Menlo";
                    hv_Fonts[2] = "Courier";
                    hv_Fonts[3] = "Courier 10 Pitch";
                    hv_Fonts[4] = "FreeMono";
                    hv_Fonts[5] = "Liberation Mono";
                    hv_Fonts[6] = "DejaVu Sans Mono";
                }
                else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("sans"))) != 0)
                {
                    hv_Fonts.Dispose();
                    hv_Fonts = new HTuple();
                    hv_Fonts[0] = "Luxi Sans";
                    hv_Fonts[1] = "DejaVu Sans";
                    hv_Fonts[2] = "FreeSans";
                    hv_Fonts[3] = "Arial";
                    hv_Fonts[4] = "Liberation Sans";
                }
                else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("serif"))) != 0)
                {
                    hv_Fonts.Dispose();
                    hv_Fonts = new HTuple();
                    hv_Fonts[0] = "Times New Roman";
                    hv_Fonts[1] = "Luxi Serif";
                    hv_Fonts[2] = "DejaVu Serif";
                    hv_Fonts[3] = "FreeSerif";
                    hv_Fonts[4] = "Utopia";
                    hv_Fonts[5] = "Liberation Serif";
                }
                else
                {
                    hv_Fonts.Dispose();
                    hv_Fonts = new HTuple(hv_Font_COPY_INP_TMP);
                }
                hv_Style.Dispose();
                hv_Style = "";
                if ((int)(new HTuple(hv_Bold.TupleEqual("true"))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_Style = hv_Style + "Bold";
                            hv_Style.Dispose();
                            hv_Style = ExpTmpLocalVar_Style;
                        }
                    }
                }
                else if ((int)(new HTuple(hv_Bold.TupleNotEqual("false"))) != 0)
                {
                    hv_Exception.Dispose();
                    hv_Exception = "Wrong value of control parameter Bold";
                    throw new HalconException(hv_Exception);
                }
                if ((int)(new HTuple(hv_Slant.TupleEqual("true"))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_Style = hv_Style + "Italic";
                            hv_Style.Dispose();
                            hv_Style = ExpTmpLocalVar_Style;
                        }
                    }
                }
                else if ((int)(new HTuple(hv_Slant.TupleNotEqual("false"))) != 0)
                {
                    hv_Exception.Dispose();
                    hv_Exception = "Wrong value of control parameter Slant";
                    throw new HalconException(hv_Exception);
                }
                if ((int)(new HTuple(hv_Style.TupleEqual(""))) != 0)
                {
                    hv_Style.Dispose();
                    hv_Style = "Normal";
                }
                hv_AvailableFonts.Dispose();
                HOperatorSet.QueryFont(hv_WindowHandle, out hv_AvailableFonts);
                hv_Font_COPY_INP_TMP.Dispose();
                hv_Font_COPY_INP_TMP = "";
                for (hv_Fdx = 0; (int)hv_Fdx <= (int)((new HTuple(hv_Fonts.TupleLength())) - 1); hv_Fdx = (int)hv_Fdx + 1)
                {
                    hv_Indices.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_Indices = hv_AvailableFonts.TupleFind(
                            hv_Fonts.TupleSelect(hv_Fdx));
                    }
                    if ((int)(new HTuple((new HTuple(hv_Indices.TupleLength())).TupleGreater(
                        0))) != 0)
                    {
                        if ((int)(new HTuple(((hv_Indices.TupleSelect(0))).TupleGreaterEqual(0))) != 0)
                        {
                            hv_Font_COPY_INP_TMP.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Font_COPY_INP_TMP = hv_Fonts.TupleSelect(
                                    hv_Fdx);
                            }
                            break;
                        }
                    }
                }
                if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual(""))) != 0)
                {
                    throw new HalconException("Wrong value of control parameter Font");
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    {
                        HTuple
                          ExpTmpLocalVar_Font = (((hv_Font_COPY_INP_TMP + "-") + hv_Style) + "-") + hv_Size_COPY_INP_TMP;
                        hv_Font_COPY_INP_TMP.Dispose();
                        hv_Font_COPY_INP_TMP = ExpTmpLocalVar_Font;
                    }
                }
                HOperatorSet.SetFont(hv_WindowHandle, hv_Font_COPY_INP_TMP);

                hv_Font_COPY_INP_TMP.Dispose();
                hv_Size_COPY_INP_TMP.Dispose();
                hv_OS.Dispose();
                hv_Fonts.Dispose();
                hv_Style.Dispose();
                hv_Exception.Dispose();
                hv_AvailableFonts.Dispose();
                hv_Fdx.Dispose();
                hv_Indices.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {

                hv_Font_COPY_INP_TMP.Dispose();
                hv_Size_COPY_INP_TMP.Dispose();
                hv_OS.Dispose();
                hv_Fonts.Dispose();
                hv_Style.Dispose();
                hv_Exception.Dispose();
                hv_AvailableFonts.Dispose();
                hv_Fdx.Dispose();
                hv_Indices.Dispose();

                throw HDevExpDefaultException;
            }
        }

        // Local procedures 
        public void display_recognition_alphabet(HTuple hv_RecognitionAlphabet, HTuple hv_WindowHandle)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_Text = new HTuple(), hv_Number = new HTuple();
            HTuple hv_Textline = new HTuple(), hv_i = new HTuple();
            HTuple hv_Minor = new HTuple(), hv_Capital = new HTuple();
            HTuple hv_Special = new HTuple(), hv_Line = new HTuple();
            HTuple hv_c = new HTuple(), hv_Char = new HTuple(), hv_Row = new HTuple();
            HTuple hv_Column = new HTuple(), hv_Width = new HTuple();
            HTuple hv_Height = new HTuple(), hv_TextWidth = new HTuple();
            HTuple hv_TextHeight = new HTuple(), hv__ = new HTuple();
            HTuple hv_LineWidth = new HTuple(), hv_LineHeight = new HTuple();
            HTuple hv_WindowWidth = new HTuple(), hv_WindowHeight = new HTuple();
            // Initialize local and output iconic variables 
            try
            {
                //Display the recognition alphabet in the given window handle.
                //
                hv_Text.Dispose();
                hv_Text = "Characters the model can recognize:";
                if (hv_Text == null)
                    hv_Text = new HTuple();
                hv_Text[new HTuple(hv_Text.TupleLength())] = "";
                //
                //Sort the characters and set the text to be displayed.
                //1) 0-9
                hv_Number.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_Number = ((hv_RecognitionAlphabet.TupleRegexpMatch(
                        "[0-9]"))).TupleUniq();
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    {
                        HTuple
                          ExpTmpLocalVar_Number = hv_Number.TupleRemove(
                            hv_Number.TupleFind(""));
                        hv_Number.Dispose();
                        hv_Number = ExpTmpLocalVar_Number;
                    }
                }
                hv_Textline.Dispose();
                hv_Textline = "";
                for (hv_i = 0; (int)hv_i <= (int)((new HTuple(hv_Number.TupleLength())) - 1); hv_i = (int)hv_i + 1)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_Textline = (hv_Textline + (hv_Number.TupleSelect(
                                hv_i))) + " ";
                            hv_Textline.Dispose();
                            hv_Textline = ExpTmpLocalVar_Textline;
                        }
                    }
                }
                if (hv_Text == null)
                    hv_Text = new HTuple();
                hv_Text[new HTuple(hv_Text.TupleLength())] = hv_Textline;
                //2) a-z
                hv_Minor.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_Minor = ((hv_RecognitionAlphabet.TupleRegexpMatch(
                        "[a-z]"))).TupleUniq();
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    {
                        HTuple
                          ExpTmpLocalVar_Minor = hv_Minor.TupleRemove(
                            hv_Minor.TupleFind(""));
                        hv_Minor.Dispose();
                        hv_Minor = ExpTmpLocalVar_Minor;
                    }
                }
                hv_Textline.Dispose();
                hv_Textline = "";
                for (hv_i = 0; (int)hv_i <= (int)((new HTuple(hv_Minor.TupleLength())) - 1); hv_i = (int)hv_i + 1)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_Textline = (hv_Textline + (hv_Minor.TupleSelect(
                                hv_i))) + " ";
                            hv_Textline.Dispose();
                            hv_Textline = ExpTmpLocalVar_Textline;
                        }
                    }
                }
                if (hv_Text == null)
                    hv_Text = new HTuple();
                hv_Text[new HTuple(hv_Text.TupleLength())] = hv_Textline;
                //3) A-Z
                hv_Capital.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_Capital = ((hv_RecognitionAlphabet.TupleRegexpMatch(
                        "[A-Z]"))).TupleUniq();
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    {
                        HTuple
                          ExpTmpLocalVar_Capital = hv_Capital.TupleRemove(
                            hv_Capital.TupleFind(""));
                        hv_Capital.Dispose();
                        hv_Capital = ExpTmpLocalVar_Capital;
                    }
                }
                hv_Textline.Dispose();
                hv_Textline = "";
                for (hv_i = 0; (int)hv_i <= (int)((new HTuple(hv_Capital.TupleLength())) - 1); hv_i = (int)hv_i + 1)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_Textline = (hv_Textline + (hv_Capital.TupleSelect(
                                hv_i))) + " ";
                            hv_Textline.Dispose();
                            hv_Textline = ExpTmpLocalVar_Textline;
                        }
                    }
                }
                if (hv_Text == null)
                    hv_Text = new HTuple();
                hv_Text[new HTuple(hv_Text.TupleLength())] = hv_Textline;
                //4) Further characters
                hv_Special.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_Special = hv_RecognitionAlphabet.TupleDifference(
                        ((hv_Minor.TupleConcat(hv_Capital))).TupleConcat(hv_Number));
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    {
                        HTuple
                          ExpTmpLocalVar_Special = hv_Special.TupleRemove(
                            hv_Special.TupleFind(""));
                        hv_Special.Dispose();
                        hv_Special = ExpTmpLocalVar_Special;
                    }
                }
                HTuple end_val33 = (new HTuple(hv_Special.TupleLength()
                    )) - 1;
                HTuple step_val33 = new HTuple(hv_Capital.TupleLength());
                for (hv_Line = 0; hv_Line.Continue(end_val33, step_val33); hv_Line = hv_Line.TupleAdd(step_val33))
                {
                    hv_Textline.Dispose();
                    hv_Textline = "";
                    HTuple end_val35 = (new HTuple((new HTuple(hv_Capital.TupleLength()
                        )) - 1)).TupleMin2(((new HTuple(hv_Special.TupleLength())) - 1) - hv_Line);
                    HTuple step_val35 = 1;
                    for (hv_c = 0; hv_c.Continue(end_val35, step_val35); hv_c = hv_c.TupleAdd(step_val35))
                    {
                        //Some characters need special treatment for correct display.
                        hv_Char.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Char = hv_Special.TupleSelect(
                                hv_Line + hv_c);
                        }
                        if ((int)(new HTuple(hv_Char.TupleEqual("\n"))) != 0)
                        {
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                {
                                    HTuple
                                      ExpTmpLocalVar_Textline = (hv_Textline + "\\n") + " ";
                                    hv_Textline.Dispose();
                                    hv_Textline = ExpTmpLocalVar_Textline;
                                }
                            }
                        }
                        else if ((int)(new HTuple(hv_Char.TupleEqual("'"))) != 0)
                        {
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                {
                                    HTuple
                                      ExpTmpLocalVar_Textline = (hv_Textline + "'") + " ";
                                    hv_Textline.Dispose();
                                    hv_Textline = ExpTmpLocalVar_Textline;
                                }
                            }
                        }
                        else
                        {
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                {
                                    HTuple
                                      ExpTmpLocalVar_Textline = (hv_Textline + hv_Char) + " ";
                                    hv_Textline.Dispose();
                                    hv_Textline = ExpTmpLocalVar_Textline;
                                }
                            }
                        }
                    }
                    if (hv_Text == null)
                        hv_Text = new HTuple();
                    hv_Text[new HTuple(hv_Text.TupleLength())] = hv_Textline;
                }
                //
                //Adapt window size.
                hv_Row.Dispose(); hv_Column.Dispose(); hv_Width.Dispose(); hv_Height.Dispose();
                HOperatorSet.GetWindowExtents(hv_WindowHandle, out hv_Row, out hv_Column, out hv_Width,
                    out hv_Height);
                set_display_font(hv_WindowHandle, 16, "mono", "true", "false");
                hv_TextWidth.Dispose();
                hv_TextWidth = -1;
                hv_TextHeight.Dispose();
                hv_TextHeight = -1;
                for (hv_Line = 0; (int)hv_Line <= (int)((new HTuple(hv_Text.TupleLength())) - 1); hv_Line = (int)hv_Line + 1)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv__.Dispose(); hv__.Dispose(); hv_LineWidth.Dispose(); hv_LineHeight.Dispose();
                        HOperatorSet.GetStringExtents(hv_WindowHandle, hv_Text.TupleSelect(hv_Line),
                            out hv__, out hv__, out hv_LineWidth, out hv_LineHeight);
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_TextWidth = hv_TextWidth.TupleMax2(
                                hv_LineWidth);
                            hv_TextWidth.Dispose();
                            hv_TextWidth = ExpTmpLocalVar_TextWidth;
                        }
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_TextHeight = hv_TextHeight.TupleMax2(
                                hv_LineHeight);
                            hv_TextHeight.Dispose();
                            hv_TextHeight = ExpTmpLocalVar_TextHeight;
                        }
                    }
                }
                hv_WindowWidth.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_WindowWidth = hv_TextWidth + 62;
                }
                hv_WindowHeight.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_WindowHeight = (hv_TextHeight * ((new HTuple(hv_Text.TupleLength()
                        )) + 1)) + 150;
                }
                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.SetWindowExtents(HDevWindowStack.GetActive(), 0, 0, hv_WindowWidth,
                        hv_WindowHeight);
                }
                //
                //Display the text.
                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.DispText(HDevWindowStack.GetActive(), hv_Text, "window", "top",
                        "left", "white", "box", "false");
                }
                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.DispText(HDevWindowStack.GetActive(), "Press Run (F5) to continue",
                        "window", "bottom", "right", "black", "box", "true");
                }

                hv_Text.Dispose();
                hv_Number.Dispose();
                hv_Textline.Dispose();
                hv_i.Dispose();
                hv_Minor.Dispose();
                hv_Capital.Dispose();
                hv_Special.Dispose();
                hv_Line.Dispose();
                hv_c.Dispose();
                hv_Char.Dispose();
                hv_Row.Dispose();
                hv_Column.Dispose();
                hv_Width.Dispose();
                hv_Height.Dispose();
                hv_TextWidth.Dispose();
                hv_TextHeight.Dispose();
                hv__.Dispose();
                hv_LineWidth.Dispose();
                hv_LineHeight.Dispose();
                hv_WindowWidth.Dispose();
                hv_WindowHeight.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {

                hv_Text.Dispose();
                hv_Number.Dispose();
                hv_Textline.Dispose();
                hv_i.Dispose();
                hv_Minor.Dispose();
                hv_Capital.Dispose();
                hv_Special.Dispose();
                hv_Line.Dispose();
                hv_c.Dispose();
                hv_Char.Dispose();
                hv_Row.Dispose();
                hv_Column.Dispose();
                hv_Width.Dispose();
                hv_Height.Dispose();
                hv_TextWidth.Dispose();
                hv_TextHeight.Dispose();
                hv__.Dispose();
                hv_LineWidth.Dispose();
                hv_LineHeight.Dispose();
                hv_WindowWidth.Dispose();
                hv_WindowHeight.Dispose();

                throw HDevExpDefaultException;
            }
        }

        // Short Description: Display tile regions of the example image. 
        public void display_tiles(HTuple hv_WindowHandle)
        {



            // Local iconic variables 

            HObject ho_Tiles;

            // Local control variables 

            HTuple hv_TileRow1 = new HTuple(), hv_TileCol1 = new HTuple();
            HTuple hv_TileRow2 = new HTuple(), hv_TileCol2 = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Tiles);
            try
            {
                //Coordinates of the tiles. These are only for visualization.
                hv_TileRow1.Dispose();
                hv_TileRow1 = new HTuple();
                hv_TileRow1[0] = 0;
                hv_TileRow1[1] = 0;
                hv_TileRow1[2] = 0;
                hv_TileRow1[3] = 0;
                hv_TileRow1[4] = 0;
                hv_TileRow1[5] = 904;
                hv_TileRow1[6] = 904;
                hv_TileRow1[7] = 904;
                hv_TileRow1[8] = 904;
                hv_TileRow1[9] = 904;
                hv_TileRow1[10] = 1808;
                hv_TileRow1[11] = 1808;
                hv_TileRow1[12] = 1808;
                hv_TileRow1[13] = 1808;
                hv_TileRow1[14] = 1808;
                hv_TileCol1.Dispose();
                hv_TileCol1 = new HTuple();
                hv_TileCol1[0] = 0;
                hv_TileCol1[1] = 808;
                hv_TileCol1[2] = 1616;
                hv_TileCol1[3] = 2424;
                hv_TileCol1[4] = 3232;
                hv_TileCol1[5] = 0;
                hv_TileCol1[6] = 808;
                hv_TileCol1[7] = 1616;
                hv_TileCol1[8] = 2424;
                hv_TileCol1[9] = 3232;
                hv_TileCol1[10] = 0;
                hv_TileCol1[11] = 808;
                hv_TileCol1[12] = 1616;
                hv_TileCol1[13] = 2424;
                hv_TileCol1[14] = 3232;
                hv_TileRow2.Dispose();
                hv_TileRow2 = new HTuple();
                hv_TileRow2[0] = 1023;
                hv_TileRow2[1] = 1023;
                hv_TileRow2[2] = 1023;
                hv_TileRow2[3] = 1023;
                hv_TileRow2[4] = 1023;
                hv_TileRow2[5] = 1927;
                hv_TileRow2[6] = 1927;
                hv_TileRow2[7] = 1927;
                hv_TileRow2[8] = 1927;
                hv_TileRow2[9] = 1927;
                hv_TileRow2[10] = 2831;
                hv_TileRow2[11] = 2831;
                hv_TileRow2[12] = 2831;
                hv_TileRow2[13] = 2831;
                hv_TileRow2[14] = 2831;
                hv_TileCol2.Dispose();
                hv_TileCol2 = new HTuple();
                hv_TileCol2[0] = 1023;
                hv_TileCol2[1] = 1831;
                hv_TileCol2[2] = 2639;
                hv_TileCol2[3] = 3447;
                hv_TileCol2[4] = 4255;
                hv_TileCol2[5] = 1023;
                hv_TileCol2[6] = 1831;
                hv_TileCol2[7] = 2639;
                hv_TileCol2[8] = 3447;
                hv_TileCol2[9] = 4255;
                hv_TileCol2[10] = 1023;
                hv_TileCol2[11] = 1831;
                hv_TileCol2[12] = 2639;
                hv_TileCol2[13] = 3447;
                hv_TileCol2[14] = 4255;
                //Generate and display rectangular regions.
                ho_Tiles.Dispose();
                HOperatorSet.GenRectangle1(out ho_Tiles, hv_TileRow1, hv_TileCol1, hv_TileRow2,
                    hv_TileCol2);
                HDevWindowStack.SetActive(hv_WindowHandle);
                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.SetDraw(HDevWindowStack.GetActive(), "margin");
                }
                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.SetColored(HDevWindowStack.GetActive(), 12);
                }
                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.DispObj(ho_Tiles, HDevWindowStack.GetActive());
                }
                //
                ho_Tiles.Dispose();

                hv_TileRow1.Dispose();
                hv_TileCol1.Dispose();
                hv_TileRow2.Dispose();
                hv_TileCol2.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {
                ho_Tiles.Dispose();

                hv_TileRow1.Dispose();
                hv_TileCol1.Dispose();
                hv_TileRow2.Dispose();
                hv_TileCol2.Dispose();

                throw HDevExpDefaultException;
            }
        }

        // Short Description: Retrieve a deep learning device to work with. 
        public void get_inference_dl_device(HTuple hv_UseFastAI2Devices, out HTuple hv_DLDevice)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_GenParamName = new HTuple(), hv_GenParamValue = new HTuple();
            HTuple hv_AIIdx = new HTuple(), hv_DLDevices = new HTuple();
            HTuple hv_P = new HTuple(), hv_DLDeviceHandles = new HTuple();
            HTuple hv_DLDeviceAI = new HTuple(), hv_DLDeviceType = new HTuple();
            // Initialize local and output iconic variables 
            hv_DLDevice = new HTuple();
            try
            {
                //This procedure retrieves an available deep learning device that can
                //be used for inference by apply_deep_ocr. It tries to choose the faster
                //device type following this order: tensorrt, gpu, openvino and cpu.
                //
                //Generic parameters for inference devices sorted by speed.
                hv_GenParamName.Dispose();
                hv_GenParamName = new HTuple();
                hv_GenParamName[0] = "ai_accelerator_interface";
                hv_GenParamName[1] = "runtime";
                hv_GenParamName[2] = "ai_accelerator_interface";
                hv_GenParamName[3] = "runtime";
                hv_GenParamValue.Dispose();
                hv_GenParamValue = new HTuple();
                hv_GenParamValue[0] = "tensorrt";
                hv_GenParamValue[1] = "gpu";
                hv_GenParamValue[2] = "openvino";
                hv_GenParamValue[3] = "cpu";
                if ((int)(new HTuple(hv_UseFastAI2Devices.TupleEqual("false"))) != 0)
                {
                    hv_AIIdx.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_AIIdx = hv_GenParamName.TupleFind(
                            "ai_accelerator_interface");
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_GenParamName = hv_GenParamName.TupleRemove(
                                hv_AIIdx);
                            hv_GenParamName.Dispose();
                            hv_GenParamName = ExpTmpLocalVar_GenParamName;
                        }
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_GenParamValue = hv_GenParamValue.TupleRemove(
                                hv_AIIdx);
                            hv_GenParamValue.Dispose();
                            hv_GenParamValue = ExpTmpLocalVar_GenParamValue;
                        }
                    }
                }
                //
                //Get the deep learning inference device.
                hv_DLDevices.Dispose();
                hv_DLDevices = new HTuple();
                for (hv_P = 0; (int)hv_P <= (int)((new HTuple(hv_GenParamName.TupleLength())) - 1); hv_P = (int)hv_P + 1)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_DLDeviceHandles.Dispose();
                        HOperatorSet.QueryAvailableDlDevices(hv_GenParamName.TupleSelect(hv_P), hv_GenParamValue.TupleSelect(
                            hv_P), out hv_DLDeviceHandles);
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_DLDevices = hv_DLDevices.TupleConcat(
                                hv_DLDeviceHandles);
                            hv_DLDevices.Dispose();
                            hv_DLDevices = ExpTmpLocalVar_DLDevices;
                        }
                    }
                    if ((int)(new HTuple(hv_DLDevices.TupleNotEqual(new HTuple()))) != 0)
                    {
                        break;
                    }
                }
                if ((int)(new HTuple(hv_DLDevices.TupleEqual(new HTuple()))) != 0)
                {
                    throw new HalconException("No supported deep learning device found");
                }
                hv_DLDevice.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_DLDevice = hv_DLDevices.TupleSelect(
                        0);
                }
                //
                //In case of CPU the number of threads impacts the example duration.
                hv_DLDeviceAI.Dispose();
                HOperatorSet.GetDlDeviceParam(hv_DLDevice, "ai_accelerator_interface", out hv_DLDeviceAI);
                hv_DLDeviceType.Dispose();
                HOperatorSet.GetDlDeviceParam(hv_DLDevice, "type", out hv_DLDeviceType);
                if ((int)((new HTuple(hv_DLDeviceAI.TupleEqual("none"))).TupleAnd(new HTuple(hv_DLDeviceType.TupleEqual(
                    "cpu")))) != 0)
                {
                    HOperatorSet.SetSystem("thread_num", 4);
                }
                //

                hv_GenParamName.Dispose();
                hv_GenParamValue.Dispose();
                hv_AIIdx.Dispose();
                hv_DLDevices.Dispose();
                hv_P.Dispose();
                hv_DLDeviceHandles.Dispose();
                hv_DLDeviceAI.Dispose();
                hv_DLDeviceType.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {

                hv_GenParamName.Dispose();
                hv_GenParamValue.Dispose();
                hv_AIIdx.Dispose();
                hv_DLDevices.Dispose();
                hv_P.Dispose();
                hv_DLDeviceHandles.Dispose();
                hv_DLDeviceAI.Dispose();
                hv_DLDeviceType.Dispose();

                throw HDevExpDefaultException;
            }
        }

        public void Cyclic()
        {
            while (!ClosingApplication)
            {

                if (Type == 0)
                {
                    if (TestCounter > 0)
                    { TestCounter = 0; }
                    GrabFromGigE();
                }
                else
                {
                    if (TestCounter > 500)
                    { TestCounter = 0; }


                }
                TestCounter++;
                Thread.Sleep(1);
            }

        }
        public void DetectShape()
        {
            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_Image, ho_ModelRegion, ho_TemplateImage;
            HObject ho_ModelContours, ho_TransContours, ho_MatchContour = null;

            // Local control variables 

            HTuple hv_ModelID = new HTuple(), hv_ModelRegionArea = new HTuple();
            HTuple hv_RefRow = new HTuple(), hv_RefColumn = new HTuple();
            HTuple hv_HomMat2D = new HTuple(), hv_MatchResultID = new HTuple();
            HTuple hv_NumMatchResult = new HTuple(), hv_I = new HTuple();
            HTuple hv_Row = new HTuple(), hv_Column = new HTuple();
            HTuple hv_Angle = new HTuple(), hv_ScaleRow = new HTuple();
            HTuple hv_ScaleColumn = new HTuple(), hv_Score = new HTuple();

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Image);
            HOperatorSet.GenEmptyObj(out ho_ModelRegion);
            HOperatorSet.GenEmptyObj(out ho_TemplateImage);
            HOperatorSet.GenEmptyObj(out ho_ModelContours);
            HOperatorSet.GenEmptyObj(out ho_TransContours);
            HOperatorSet.GenEmptyObj(out ho_MatchContour);
            //Image Acquisition 01: Code generated by Image Acquisition 01
            ho_Image.Dispose();
            //
            Type = 1;
            if (Type == 0)
            {
                HOperatorSet.ReadImage(out ho_Image, "C:/JOLT/FHV7.jpg");
            }
            else
            {
                HOperatorSet.ReadImage(out ho_Image, "C:/Users/joltm/OneDrive/Afbeeldingen/1_TEACH 001.jpg");
            }

            //
            //Matching 01: ************************************************
            //Matching 01: BEGIN of generated code for model initialization
            //Matching 01: ************************************************
            //
            //Matching 01: Obtain the model image
            //Matching 01: The image is assumed to be made available in the
            //Matching 01: variable last displayed in the graphics window
            {
                HObject ExpTmpOutVar_0;
                HOperatorSet.CopyObj(ho_Image, out ExpTmpOutVar_0, 1, 1);
                ho_Image.Dispose();
                ho_Image = ExpTmpOutVar_0;
            }
            //
            //Matching 01: Build the ROI from basic regions
            ho_ModelRegion.Dispose();
            HOperatorSet.GenRectangle1(out ho_ModelRegion, 400, 400, 600, 600);
            //
            //Matching 01: Reduce the model template
            ho_TemplateImage.Dispose();
            HOperatorSet.ReduceDomain(ho_Image, ho_ModelRegion, out ho_TemplateImage);
            //
            //Matching 01: Create and train the shape model
            hv_ModelID.Dispose();
            HOperatorSet.CreateGenericShapeModel(out hv_ModelID);
            //Matching 01: set the model parameters
            HOperatorSet.SetGenericShapeModelParam(hv_ModelID, "metric", "use_polarity");
            HOperatorSet.TrainGenericShapeModel(ho_TemplateImage, hv_ModelID);
            //
            //Matching 01: Get the model contour for transforming it later into the image
            ho_ModelContours.Dispose();
            HOperatorSet.GetShapeModelContours(out ho_ModelContours, hv_ModelID, 1);
            //
            //Matching 01: Support for displaying the model
            //Matching 01: Get the reference position
            hv_ModelRegionArea.Dispose(); hv_RefRow.Dispose(); hv_RefColumn.Dispose();
            HOperatorSet.AreaCenter(ho_ModelRegion, out hv_ModelRegionArea, out hv_RefRow,
                out hv_RefColumn);
            hv_HomMat2D.Dispose();
            HOperatorSet.VectorAngleToRigid(0, 0, 0, hv_RefRow, hv_RefColumn, 0, out hv_HomMat2D);
            ho_TransContours.Dispose();
            HOperatorSet.AffineTransContourXld(ho_ModelContours, out ho_TransContours, hv_HomMat2D);
            //
            //Matching 01: Display the model contours
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_Image, HDevWindowStack.GetActive());

            }
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.SetColor(HDevWindowStack.GetActive(), "green");
            }
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.SetDraw(HDevWindowStack.GetActive(), "margin");
            }
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_ModelRegion, HDevWindowStack.GetActive());
            }
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_TransContours, HDevWindowStack.GetActive());
            }
            // stop(...); only in hdevelop
            //
            //Matching 01: END of generated code for model initialization
            //Matching 01:  * * * * * * * * * * * * * * * * * * * * * * *
            //Matching 01: BEGIN of generated code for model application
            //Matching 01: Set the search paramaters
            HOperatorSet.SetGenericShapeModelParam(hv_ModelID, "border_shape_models", "false");
            //Matching 01: The following operations are usually moved into
            //Matching 01: that loop where the acquired images are processed
            //
            //Matching 01: Find the model
            hv_MatchResultID.Dispose(); hv_NumMatchResult.Dispose();
            HOperatorSet.FindGenericShapeModel(ho_Image, hv_ModelID, out hv_MatchResultID,
                out hv_NumMatchResult);
            //
            //Matching 01: Retrieve results
            HTuple end_val53 = hv_NumMatchResult - 1;
            HTuple step_val53 = 1;
            for (hv_I = 0; hv_I.Continue(end_val53, step_val53); hv_I = hv_I.TupleAdd(step_val53))
            {
                //
                //Matching 01: Display the detected match
                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.DispObj(ho_Image, HDevWindowStack.GetActive());
                }
                ho_MatchContour.Dispose();
                HOperatorSet.GetGenericShapeModelResultObject(out ho_MatchContour, hv_MatchResultID,
                    hv_I, "contours");
                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.SetColor(HDevWindowStack.GetActive(), "green");
                }
                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.DispObj(ho_MatchContour, HDevWindowStack.GetActive());
                }
                ShapeSearch.ResultImage = ho_Image;
                ShapeSearch.ResultRegion = ho_MatchContour;
                //ResultTransform = ho_TransContours;
                //
                //Matching 01: Retrieve parameters of the detected match
                hv_Row.Dispose();
                HOperatorSet.GetGenericShapeModelResult(hv_MatchResultID, hv_I, "row", out hv_Row);
                hv_Column.Dispose();
                HOperatorSet.GetGenericShapeModelResult(hv_MatchResultID, hv_I, "column", out hv_Column);
                hv_Angle.Dispose();
                HOperatorSet.GetGenericShapeModelResult(hv_MatchResultID, hv_I, "angle", out hv_Angle);
                hv_ScaleRow.Dispose();
                HOperatorSet.GetGenericShapeModelResult(hv_MatchResultID, hv_I, "scale_row",
                    out hv_ScaleRow);
                hv_ScaleColumn.Dispose();
                HOperatorSet.GetGenericShapeModelResult(hv_MatchResultID, hv_I, "scale_column",
                    out hv_ScaleColumn);
                hv_HomMat2D.Dispose();
                HOperatorSet.GetGenericShapeModelResult(hv_MatchResultID, hv_I, "hom_mat_2d",
                    out hv_HomMat2D);
                hv_Score.Dispose();
                HOperatorSet.GetGenericShapeModelResult(hv_MatchResultID, hv_I, "score", out hv_Score);
                // stop(...); only in hdevelop
            }
            //
            //Matching 01: *******************************************
            //Matching 01: END of generated code for model application
            //Matching 01: *******************************************
            //
            ho_Image.Dispose();
            ho_ModelRegion.Dispose();
            ho_TemplateImage.Dispose();
            ho_ModelContours.Dispose();
            ho_TransContours.Dispose();
            ho_MatchContour.Dispose();

            hv_ModelID.Dispose();
            hv_ModelRegionArea.Dispose();
            hv_RefRow.Dispose();
            hv_RefColumn.Dispose();
            hv_HomMat2D.Dispose();
            hv_MatchResultID.Dispose();
            hv_NumMatchResult.Dispose();
            hv_I.Dispose();
            hv_Row.Dispose();
            hv_Column.Dispose();
            hv_Angle.Dispose();
            hv_ScaleRow.Dispose();
            hv_ScaleColumn.Dispose();
            hv_Score.Dispose();
        }
        public void GrabFromUSB3()
        {
            if (framegrabber == null)
            {
                framegrabber = new HFramegrabber(
                "USB3Vision", 0, 0, 0, 0, 0, 0, "progressive",
                -1, "default", -1, "false", "default",
            "User Defined Name", 0, -1
            );
            }
            framegrabber.SetFramegrabberParam("ExposureMode", "Timed");
            framegrabber.SetFramegrabberParam("ExposureTime", 3000);


            HObject ho_Image = new HObject();
            HOperatorSet.GenEmptyObj(out ho_Image);
            HOperatorSet.GrabImage(out ho_Image, framegrabber);
            HOperatorSet.RotateImage(ho_Image, out ho_Image, 180, "constant");

            Grab.Image = ho_Image;

        }
        public void GrabFromGigE()
        {
            if (busy == true)
            {
                return;
            }
            try
            {
                if (framegrabber == null)
                {
                    framegrabber = new HFramegrabber(
                    "GigEVision2", 0, 0, 0, 0, 0, 0, "progressive", -1, "default", -1, "false", "default", "d47c4431528b_OMRONSENTECH_STCMCS122BPOE", 0, -1
                );  // d47c443154af_OMRONSENTECH_STCMBS122BPOE kaartenteller

                }
                framegrabber.SetFramegrabberParam("ExposureMode", "Timed");
                framegrabber.SetFramegrabberParam("ExposureTime", 10000);
                

                HObject ho_Image = new HObject();
                HOperatorSet.GenEmptyObj(out ho_Image);
                try
                {

                    HOperatorSet.GrabImage(out ho_Image, framegrabber);
                    HOperatorSet.RotateImage(ho_Image, out ho_Image, 90, "constant");
                    Grab.Image = ho_Image;
                    busy = true;

                    if (deepOcrInitialized == true)
                    {
                        DeepOcrDetect(ho_Image);

                    }
                    else
                    {
                        DeepOcrInit();
                        DeepOcrDetect(ho_Image);

                    }
                    busy = false;
                    
                }
                catch (Exception ex)
                {

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);                
            }
        }

        private void action()
        {


            // Local iconic variables 

            HObject ho_Image = null;

            // Local control variables 

            HTuple hv_PathExample = new HTuple(), hv_ImagesPath = new HTuple();
            HTuple hv_UseFastAI2Devices = new HTuple(), hv_DLDevice = new HTuple();
            HTuple hv_DeepOcrHandle = new HTuple(), hv_RecognitionAlphabet = new HTuple();
            HTuple hv_DisplaySize = new HTuple(), hv_WindowHandleAlphabet = new HTuple();
            HTuple hv_PreprocessedDisplayWidth = new HTuple(), hv_PreprocessedDisplayHeight = new HTuple();
            HTuple hv_ScoreMapsDisplayWidth = new HTuple(), hv_ScoreMapsDisplayHeight = new HTuple();
            HTuple hv_WindowHandle = new HTuple(), hv_WindowPreprocessed = new HTuple();
            HTuple hv_WindowScoreMaps = new HTuple(), hv_Index = new HTuple();
            HTuple hv_StartTime = new HTuple(), hv_Width = new HTuple();
            HTuple hv_Height = new HTuple(), hv_DeepOcrResult = new HTuple();
            HTuple hv_EndTime = new HTuple(), hv_TimeTaken = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Image);
            try
            {
                //
                //This example shows the usage of the Deep OCR:
                //- Part 1: Detection and recognition of the words within an image.
                //- Part 2: Recognition of the words only.
                //- Part 3: Detection of the words only.
                //- Part 4: Detection and recognition on a large image with automatic tiling.
                //
                dev_update_off();
                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.CloseWindow(HDevWindowStack.Pop());
                }
                //
                //Set path for images.
                hv_PathExample.Dispose();
                hv_PathExample = "ocr/";
                hv_ImagesPath.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_ImagesPath = hv_PathExample + (((((((((
                        (new HTuple("card01.bmp")).TupleConcat("card02.bmp")).TupleConcat("card03.bmp")).TupleConcat(
                        "card04.bmp")).TupleConcat("card05")).TupleConcat("card06")).TupleConcat(
                        "card07")).TupleConcat("card08")).TupleConcat("card09")).TupleConcat("card10"));
                }
                //
                //In general, optimal runtime performance and minimal memory usage is
                //obtained by using an AI² device for inference (e.g. TensorRT/OpenVINO).
                //Note, however the initialization time increases if this is set to 'true'.
                hv_UseFastAI2Devices.Dispose();
                hv_UseFastAI2Devices = "true";

                //
                //Get the inference deep learning device.
                hv_DLDevice.Dispose();
                get_inference_dl_device(hv_UseFastAI2Devices, out hv_DLDevice);


                //*************************************************************
                //Part 1: Detection and recognition of words within an image.
                //*************************************************************
                //Create the model.
                hv_DeepOcrHandle.Dispose();
                HOperatorSet.CreateDeepOcr(new HTuple(), new HTuple(), out hv_DeepOcrHandle);
                //
                //Return the list of characters for which the model has been trained.
                hv_RecognitionAlphabet.Dispose();
                HOperatorSet.GetDeepOcrParam(hv_DeepOcrHandle, "recognition_alphabet", out hv_RecognitionAlphabet);
                //
                //Set the inference deep learning device after the model is configurated.

                HOperatorSet.SetDeepOcrParam(hv_DeepOcrHandle, "device", hv_DLDevice);

                //
                hv_DisplaySize.Dispose();
                hv_DisplaySize = 480;
                //
                HOperatorSet.SetWindowAttr("background_color", "black");
                HOperatorSet.OpenWindow(0, 0, hv_DisplaySize, hv_DisplaySize, 0, "visible", "", out hv_WindowHandleAlphabet);
                HDevWindowStack.Push(hv_WindowHandleAlphabet);
                display_recognition_alphabet(hv_RecognitionAlphabet, hv_WindowHandleAlphabet);
                // stop(...); only in hdevelop
                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.CloseWindow(HDevWindowStack.Pop());
                }
                //
                //For a prettier visualization, the preprocessed images and scores are
                //shown with a reduced size.
                hv_PreprocessedDisplayWidth.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_PreprocessedDisplayWidth = 0.5 * hv_DisplaySize;
                }
                hv_PreprocessedDisplayHeight.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_PreprocessedDisplayHeight = 0.5 * hv_DisplaySize;
                }
                hv_ScoreMapsDisplayWidth.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_ScoreMapsDisplayWidth = 2 * hv_PreprocessedDisplayWidth;
                }
                hv_ScoreMapsDisplayHeight.Dispose();
                hv_ScoreMapsDisplayHeight = new HTuple(hv_PreprocessedDisplayHeight);
                //
                HOperatorSet.SetWindowAttr("background_color", "black");
                HOperatorSet.OpenWindow(0, 0, hv_DisplaySize, hv_DisplaySize, 0, "visible", "", out hv_WindowHandle);
                HDevWindowStack.Push(hv_WindowHandle);
                HOperatorSet.SetWindowAttr("background_color", "black");
                HOperatorSet.OpenWindow(0, 10 + hv_DisplaySize, hv_PreprocessedDisplayWidth, hv_PreprocessedDisplayHeight, 0, "visible", "", out hv_WindowPreprocessed);
                HDevWindowStack.Push(hv_WindowPreprocessed);
                HOperatorSet.SetWindowAttr("background_color", "black");
                HOperatorSet.OpenWindow(0, (20 + hv_DisplaySize) + hv_PreprocessedDisplayWidth, hv_ScoreMapsDisplayWidth, hv_ScoreMapsDisplayHeight, 0, "visible", "", out hv_WindowScoreMaps);
                HDevWindowStack.Push(hv_WindowScoreMaps);
                //
                //Apply Deep OCR on different images.
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_ImagesPath.TupleLength()
                    )) - 1); hv_Index = (int)hv_Index + 1)
                {
                    hv_StartTime.Dispose();
                    HOperatorSet.CountSeconds(out hv_StartTime);
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        ho_Image.Dispose();
                        HOperatorSet.ReadImage(out ho_Image, hv_ImagesPath.TupleSelect(hv_Index));
                    }
                    hv_Width.Dispose(); hv_Height.Dispose();
                    HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
                    HDevWindowStack.SetActive(hv_WindowHandle);
                    if (HDevWindowStack.IsOpen())
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            HOperatorSet.SetWindowExtents(HDevWindowStack.GetActive(), 0, 0, hv_DisplaySize,
                                (hv_DisplaySize * hv_Height) / hv_Width);
                        }
                    }
                    if (HDevWindowStack.IsOpen())
                    {
                        HOperatorSet.DispObj(ho_Image, HDevWindowStack.GetActive());
                    }
                    //
                    //Detect and recognize the words.
                    hv_DeepOcrResult.Dispose();
                    HOperatorSet.ApplyDeepOcr(ho_Image, hv_DeepOcrHandle, "auto", out hv_DeepOcrResult);
                    //
                    //
                    //Visualize the results on the original image.
                    //The detections are shown as oriented rectangles, where an arrow indicates the reading direction.
                    //The recognized words are shown at a corner of the detection rectangles. For further information,
                    //please see also the documentation of the procedure "dev_display_deep_ocr_results".
                    dev_display_deep_ocr_results(ho_Image, hv_WindowHandle, hv_DeepOcrResult,
                        new HTuple(), new HTuple());
                    
                    if (HDevWindowStack.IsOpen())
                    {
                        HOperatorSet.DispText(HDevWindowStack.GetActive(), "Original image", "window",
                            "top", "left", "black", new HTuple(), new HTuple());
                    }
                    //
                    //Visualize the preprocessed image with the localized words as well as the
                    //respective character and link score maps.
                    dev_display_deep_ocr_results_preprocessed(hv_WindowPreprocessed, hv_DeepOcrResult,
                        new HTuple(), new HTuple());
                    dev_display_deep_ocr_score_maps(hv_WindowScoreMaps, hv_DeepOcrResult, new HTuple(),
                        new HTuple());
                    if (HDevWindowStack.IsOpen())
                    {
                        HOperatorSet.DispText(HDevWindowStack.GetActive(), "Press Run (F5) to continue",
                            "window", "bottom", "right", "black", new HTuple(), new HTuple());
                    }
                    hv_EndTime.Dispose();
                    HOperatorSet.CountSeconds(out hv_EndTime);
                    Grab.Image = ho_Image;
                    hv_TimeTaken.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_TimeTaken = hv_EndTime - hv_StartTime;
                    }
                    // stop(...); only in hdevelop
                    //
                    if (HDevWindowStack.IsOpen())
                    {
                        HOperatorSet.ClearWindow(HDevWindowStack.GetActive());
                    }
                    HDevWindowStack.SetActive(hv_WindowPreprocessed);
                    if (HDevWindowStack.IsOpen())
                    {
                        HOperatorSet.ClearWindow(HDevWindowStack.GetActive());
                    }
                    HDevWindowStack.SetActive(hv_WindowScoreMaps);
                    if (HDevWindowStack.IsOpen())
                    {
                        HOperatorSet.ClearWindow(HDevWindowStack.GetActive());
                    }
                    if (HDevWindowStack.IsOpen())
                    {
                        HOperatorSet.SetLut(HDevWindowStack.GetActive(), "default");
                    }
                }
                //
                HDevWindowStack.SetActive(hv_WindowScoreMaps);
                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.CloseWindow(HDevWindowStack.Pop());
                }
                HDevWindowStack.SetActive(hv_WindowPreprocessed);
                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.CloseWindow(HDevWindowStack.Pop());
                }
                HDevWindowStack.SetActive(hv_WindowHandle);
                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.CloseWindow(HDevWindowStack.Pop());
                }
                HOperatorSet.ClearHandle(hv_DeepOcrHandle);

            }
            catch (HalconException HDevExpDefaultException)
            {
                ho_Image.Dispose();

                hv_PathExample.Dispose();
                hv_ImagesPath.Dispose();
                hv_UseFastAI2Devices.Dispose();
                hv_DLDevice.Dispose();
                hv_DeepOcrHandle.Dispose();
                hv_RecognitionAlphabet.Dispose();
                hv_DisplaySize.Dispose();
                hv_WindowHandleAlphabet.Dispose();
                hv_PreprocessedDisplayWidth.Dispose();
                hv_PreprocessedDisplayHeight.Dispose();
                hv_ScoreMapsDisplayWidth.Dispose();
                hv_ScoreMapsDisplayHeight.Dispose();
                hv_WindowHandle.Dispose();
                hv_WindowPreprocessed.Dispose();
                hv_WindowScoreMaps.Dispose();
                hv_Index.Dispose();
                hv_StartTime.Dispose();
                hv_Width.Dispose();
                hv_Height.Dispose();
                hv_DeepOcrResult.Dispose();
                hv_EndTime.Dispose();
                hv_TimeTaken.Dispose();

                throw HDevExpDefaultException;
            }
            ho_Image.Dispose();

            hv_PathExample.Dispose();
            hv_ImagesPath.Dispose();
            hv_UseFastAI2Devices.Dispose();
            hv_DLDevice.Dispose();
            hv_DeepOcrHandle.Dispose();
            hv_RecognitionAlphabet.Dispose();
            hv_DisplaySize.Dispose();
            hv_WindowHandleAlphabet.Dispose();
            hv_PreprocessedDisplayWidth.Dispose();
            hv_PreprocessedDisplayHeight.Dispose();
            hv_ScoreMapsDisplayWidth.Dispose();
            hv_ScoreMapsDisplayHeight.Dispose();
            hv_WindowHandle.Dispose();
            hv_WindowPreprocessed.Dispose();
            hv_WindowScoreMaps.Dispose();
            hv_Index.Dispose();
            hv_StartTime.Dispose();
            hv_Width.Dispose();
            hv_Height.Dispose();
            hv_DeepOcrResult.Dispose();
            hv_EndTime.Dispose();
            hv_TimeTaken.Dispose();

        }

        public void DeepOcrInit()
        {
            hv_UseFastAI2Devices = "false";
            get_inference_dl_device(hv_UseFastAI2Devices, out hv_DLDevice);
            Debug.WriteLine("Fast AI Device : " + hv_DLDevice.ToString());
            //Create the model.
            hv_DeepOcrHandle.Dispose();
            HOperatorSet.CreateDeepOcr(new HTuple(), new HTuple(), out hv_DeepOcrHandle);
            //
            Debug.WriteLine("Deep OCR model created ");
            //Return the list of characters for which the model has been trained.
            hv_RecognitionAlphabet.Dispose();
            HOperatorSet.GetDeepOcrParam(hv_DeepOcrHandle, "recognition_alphabet", out hv_RecognitionAlphabet);
            Debug.WriteLine("Deep OCR model : " + hv_RecognitionAlphabet.ToString());
            //
            //Set the inference deep learning device after the model is configurated.

            HOperatorSet.SetDeepOcrParam(hv_DeepOcrHandle, "device", hv_DLDevice);

            deepOcrInitialized = true;
            Debug.WriteLine("Deep OCR model is initialised");
        }
        public void DeepOcrDetect(HObject image)
        {

            // 1) Apply Deep OCR
            hv_DeepOcrResult.Dispose();
            HOperatorSet.ApplyDeepOcr(image, hv_DeepOcrHandle, "auto", out hv_DeepOcrResult);

            hv_ResultKey = "word_boxes_on_image";

            //get_deep_ocr_detection_word_boxes(hv_DeepOcrResult,hv_ResultKey, out hv_WordBoxRow, out hv_WordBoxColumn,out hv_WordBoxPhi,out hv_WordBoxLength1,out hv_WordBoxLength2);

            // Eerste resultaat (meestal 1 dict per image)
            HTuple hv_ResultDict = hv_DeepOcrResult.TupleSelect(0);

            // Woorden dictionary ophalen
            HTuple hv_Words;
            HOperatorSet.GetDictTuple(hv_ResultDict, "words", out hv_Words);

            // Parameters voor elke tekstregel
            HTuple hv_Texts, hv_Row, hv_Col, hv_Phi, hv_L1, hv_L2;
            HOperatorSet.GetDictTuple(hv_Words, "word", out hv_Texts);
            HOperatorSet.GetDictTuple(hv_Words, "row", out hv_Row);
            HOperatorSet.GetDictTuple(hv_Words, "col", out hv_Col);
            HOperatorSet.GetDictTuple(hv_Words, "phi", out hv_Phi);
            HOperatorSet.GetDictTuple(hv_Words, "length1", out hv_L1);
            HOperatorSet.GetDictTuple(hv_Words, "length2", out hv_L2);

            // Eén HObject maken met alle OCR-boxen
            HObject ho_AllRects;
            HOperatorSet.GenEmptyObj(out ho_AllRects);


            // 3️⃣ Vul de woordenlijst voor tekstlabels
            ResultOCRWords.Clear();

            for (int i = 0; i < hv_Texts.Length; i++)
            {
                HObject ho_Rect;
                HOperatorSet.GenRectangle2(out ho_Rect,
                    hv_Row[i], hv_Col[i], hv_Phi[i], hv_L1[i], hv_L2[i]);
                HOperatorSet.ConcatObj(ho_AllRects, ho_Rect, out ho_AllRects);
                ho_Rect.Dispose();

                ResultOCRWords.Add(new OcrWord
                {
                    Word = hv_Texts[i].S,
                    Row = hv_Row[i].D - hv_L2[i].D - 10,   // een beetje boven het boxje
                    Column = hv_Col[i].D - hv_L1[i].D
                });

                OnOcrCompleted(true);

            }




        }

        public void GrabFromFile()
        {
            HObject ho_Image = new HObject();
            HOperatorSet.GenEmptyObj(out ho_Image);

            string[] _imageFiles = Directory.GetFiles("C:/JOLT/", "*.bmp");

            if (_imageFiles.Length > 0)
            {

                // Als we aan het einde zijn: opnieuw beginnen (optioneel)
                if (fileIndex >= _imageFiles.Length)
                    fileIndex = 0;

                // Bestandspad ophalen
                string imagePath = _imageFiles[fileIndex];
                fileIndex++;

                // HALCON-image inlezen
                HOperatorSet.ReadImage(out ho_Image, imagePath);


                Grab.Image = ho_Image;
            }
        }
        public void TeachShape()
        {
            //GrabFromFile();

            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_Image, ho_ModelRegion, ho_TemplateImage;
            HObject ho_ModelContours, ho_TransContours, ho_MatchContour = null;

            // Local control variables 

            HTuple hv_ModelID = new HTuple(), hv_ModelRegionArea = new HTuple();
            HTuple hv_RefRow = new HTuple(), hv_RefColumn = new HTuple();
            HTuple hv_HomMat2D = new HTuple(), hv_MatchResultID = new HTuple();
            HTuple hv_NumMatchResult = new HTuple(), hv_I = new HTuple();
            HTuple hv_Row = new HTuple(), hv_Column = new HTuple();
            HTuple hv_Angle = new HTuple(), hv_ScaleRow = new HTuple();
            HTuple hv_ScaleColumn = new HTuple(), hv_Score = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Image);
            HOperatorSet.GenEmptyObj(out ho_ModelRegion);
            HOperatorSet.GenEmptyObj(out ho_TemplateImage);
            HOperatorSet.GenEmptyObj(out ho_ModelContours);
            HOperatorSet.GenEmptyObj(out ho_TransContours);
            HOperatorSet.GenEmptyObj(out ho_MatchContour);
            //Image Acquisition 01: Code generated by Image Acquisition 01
            ho_Image.Dispose();
            Type = 1;
            if (Type == 0)
            {
                ho_Image = Grab.Image;
                //HOperatorSet.ReadImage(out ho_Image, "C:/JOLT/FHV7.jpg");
            }
            else
            {
                HOperatorSet.ReadImage(out ho_Image, "C:/Users/joltm/OneDrive/Afbeeldingen/1_TEACH 001.jpg");
            }
            Grab.Image = ho_Image;
            //
            //Matching 01: ************************************************
            //Matching 01: BEGIN of generated code for model initialization
            //Matching 01: ************************************************
            //
            //Matching 01: Obtain the model image
            //Matching 01: The image is assumed to be made available in the
            //Matching 01: variable last displayed in the graphics window
            {
                HObject ExpTmpOutVar_0;
                HOperatorSet.CopyObj(ho_Image, out ExpTmpOutVar_0, 1, 1);
                ho_Image.Dispose();
                ho_Image = ExpTmpOutVar_0;
            }
            //
            //Matching 01: Build the ROI from basic regions
            ho_ModelRegion.Dispose();
            HOperatorSet.GenRectangle1(out ho_ModelRegion, 20, 20, 200, 200);
            //
            //Matching 01: Reduce the model template
            ho_TemplateImage.Dispose();
            HOperatorSet.ReduceDomain(ho_Image, ho_ModelRegion, out ho_TemplateImage);
            //
            //Matching 01: Create and train the shape model
            hv_ModelID.Dispose();
            HOperatorSet.CreateGenericShapeModel(out hv_ModelID);
            //Matching 01: set the model parameters
            HOperatorSet.SetGenericShapeModelParam(hv_ModelID, "metric", "use_polarity");
            HOperatorSet.TrainGenericShapeModel(ho_TemplateImage, hv_ModelID);
            //
            //Matching 01: Get the model contour for transforming it later into the image
            ho_ModelContours.Dispose();
            HOperatorSet.GetShapeModelContours(out ho_ModelContours, hv_ModelID, 1);
            //
            //Matching 01: Support for displaying the model
            //Matching 01: Get the reference position
            hv_ModelRegionArea.Dispose(); hv_RefRow.Dispose(); hv_RefColumn.Dispose();
            HOperatorSet.AreaCenter(ho_ModelRegion, out hv_ModelRegionArea, out hv_RefRow,
                out hv_RefColumn);
            hv_HomMat2D.Dispose();
            HOperatorSet.VectorAngleToRigid(0, 0, 0, hv_RefRow, hv_RefColumn, 0, out hv_HomMat2D);
            ho_TransContours.Dispose();
            HOperatorSet.AffineTransContourXld(ho_ModelContours, out ho_TransContours, hv_HomMat2D);
            //
            //Matching 01: Display the model contours
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_Image, HDevWindowStack.GetActive());

            }
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.SetColor(HDevWindowStack.GetActive(), "green");
            }
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.SetDraw(HDevWindowStack.GetActive(), "margin");
            }
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_ModelRegion, HDevWindowStack.GetActive());
            }
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_TransContours, HDevWindowStack.GetActive());
            }
            // stop(...); only in hdevelop
            //
            //Matching 01: END of generated code for model initialization
            //Matching 01:  * * * * * * * * * * * * * * * * * * * * * * *
            //Matching 01: BEGIN of generated code for model application
            //Matching 01: Set the search paramaters
            HOperatorSet.SetGenericShapeModelParam(hv_ModelID, "border_shape_models", "false");
            //Matching 01: The following operations are usually moved into
            //Matching 01: that loop where the acquired images are processed
            //
            //Matching 01: Find the model
            hv_MatchResultID.Dispose(); hv_NumMatchResult.Dispose();
            HOperatorSet.FindGenericShapeModel(ho_Image, hv_ModelID, out hv_MatchResultID,
                out hv_NumMatchResult);
            //
            //Matching 01: Retrieve results
            HTuple end_val53 = hv_NumMatchResult - 1;
            HTuple step_val53 = 1;
            for (hv_I = 0; hv_I.Continue(end_val53, step_val53); hv_I = hv_I.TupleAdd(step_val53))
            {
                //
                //Matching 01: Display the detected match
                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.DispObj(ho_Image, HDevWindowStack.GetActive());
                }
                ho_MatchContour.Dispose();
                HOperatorSet.GetGenericShapeModelResultObject(out ho_MatchContour, hv_MatchResultID,
                    hv_I, "contours");
                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.SetColor(HDevWindowStack.GetActive(), "green");
                }
                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.DispObj(ho_MatchContour, HDevWindowStack.GetActive());
                }
                ShapeSearch.ResultImage = ho_Image;
                ShapeSearch.ResultRegion = ho_MatchContour;
                //ResultTransform = ho_TransContours;
                //
                //Matching 01: Retrieve parameters of the detected match
                hv_Row.Dispose();
                HOperatorSet.GetGenericShapeModelResult(hv_MatchResultID, hv_I, "row", out hv_Row);
                hv_Column.Dispose();
                HOperatorSet.GetGenericShapeModelResult(hv_MatchResultID, hv_I, "column", out hv_Column);
                hv_Angle.Dispose();
                HOperatorSet.GetGenericShapeModelResult(hv_MatchResultID, hv_I, "angle", out hv_Angle);
                hv_ScaleRow.Dispose();
                HOperatorSet.GetGenericShapeModelResult(hv_MatchResultID, hv_I, "scale_row",
                    out hv_ScaleRow);
                hv_ScaleColumn.Dispose();
                HOperatorSet.GetGenericShapeModelResult(hv_MatchResultID, hv_I, "scale_column",
                    out hv_ScaleColumn);
                hv_HomMat2D.Dispose();
                HOperatorSet.GetGenericShapeModelResult(hv_MatchResultID, hv_I, "hom_mat_2d",
                    out hv_HomMat2D);
                hv_Score.Dispose();
                HOperatorSet.GetGenericShapeModelResult(hv_MatchResultID, hv_I, "score", out hv_Score);
                // stop(...); only in hdevelop
            }
            //
            //Matching 01: *******************************************
            //Matching 01: END of generated code for model application
            //Matching 01: *******************************************
            //
            ho_Image.Dispose();
            ho_ModelRegion.Dispose();
            ho_TemplateImage.Dispose();
            ho_ModelContours.Dispose();
            ho_TransContours.Dispose();
            ho_MatchContour.Dispose();

            hv_ModelID.Dispose();
            hv_ModelRegionArea.Dispose();
            hv_RefRow.Dispose();
            hv_RefColumn.Dispose();
            hv_HomMat2D.Dispose();
            hv_MatchResultID.Dispose();
            hv_NumMatchResult.Dispose();
            hv_I.Dispose();
            hv_Row.Dispose();
            hv_Column.Dispose();
            hv_Angle.Dispose();
            hv_ScaleRow.Dispose();
            hv_ScaleColumn.Dispose();
            hv_Score.Dispose();
        }

        #endregion

        #region Properties

        [ObservableProperty]
        ObservableCollection<OcrWord> _resultOCRWords = new ObservableCollection<OcrWord>();

        [ObservableProperty]
        private HObject _resultOCROverlay = new HObject();

        [ObservableProperty]
        private bool _closingApplication;

        [ObservableProperty]
        ShapeSearch _shapeSearch;

        [ObservableProperty]
        Grab _grab = new Grab();

        [ObservableProperty]
        int _type;

        [ObservableProperty]
        int _testCounter;

        [ObservableProperty]
        int _hardwareId;

        [ObservableProperty]
        string _ipAddress;

        [ObservableProperty]
        HWindow _hWindow = new HWindow();


        #endregion
    }
    public partial class Grab : ObservableObject
    {

        #region Commands
        #endregion

        #region Constructor
        #endregion

        #region Events
        #endregion

        #region Fields



        #endregion

        #region Methods
        #endregion

        #region Properties

        [ObservableProperty]
        HObject _image = new HObject();


        [ObservableProperty]
        int _testCounter;
        #endregion


    }
    public partial class ShapeSearch : ObservableObject
    {

        #region Commands



        #endregion

        #region Constructor

        public ShapeSearch()
        {

        }

        #endregion

        #region Events
        #endregion

        #region Fields



        #endregion

        #region Methods


        #endregion

        #region Properties

        [ObservableProperty]
        HObject _resultImage = new HObject();

        [ObservableProperty]
        HObject _resultRegion = new HObject();

        [ObservableProperty]
        HObject _resultTransform = new HObject();

        #endregion



    }
    public class OcrWord
    {
        public string Word { get; set; }
        public double Row { get; set; }
        public double Column { get; set; }
    }
}
